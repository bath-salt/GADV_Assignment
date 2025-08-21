using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Stroke
{
    public GameObject gameObject;
    public Transform startPoint;
    public Transform endPoint;
    public StrokeCollider collider;
    public GameObject StrokePreview;
}
public class TraceManager : MonoBehaviour
{
    // drives the per letter tracing flow
    // activates letter prefabs and strokes 
    // spawns marker at each stroke's start point and wires its movement constraint
    // after last stroke notifies letterspawner to go to the next letter
    public GameObject markerPrefab;
    public GameObject linePrefab;
    public LetterSpawner LetterSpawner;
    public WordSFX wordSFX;
    private List<GameObject> drawnLines = new List<GameObject>();

    private GameObject currentMarker;
    private LineRenderer currentLine;
    private StrokeCollider currentStroke;
    public ParticleSystem sparklePrefab;
    private Transform startPoint;
    private Transform endPoint;

    public float nextStrokeDelay = 0.75f;
    private bool advancingStroke = false;

    private List<Vector3> points = new List<Vector3>();

    private List<Stroke> strokes = new List<Stroke>();
    private int currentStrokeIndex = 0;
    public void SetupLetter(GameObject letter)
    {
        strokes.Clear();  //Clear list from previous letters
        currentStrokeIndex = 0;

        // iterate over each direct child Transform under the letter Gameobject
        foreach(Transform child in letter.transform)
        {
            // only process children whose names have stroke
            // ensures that we only treat intended objects as traceable strokes
            if (child.name.StartsWith("Stroke"))
            {
                // create a new Stroke data containter to hold all components 
                // get everything a stroke should have like start point, end point collider 
                Stroke s = new Stroke();
                s.gameObject = child.gameObject;
                s.startPoint = child.Find("StartPoint");
                s.endPoint = child.Find("EndPoint");
                s.collider = child.GetComponent<StrokeCollider>();
                s.StrokePreview = child.Find("StrokePreview")?.gameObject;

                // add the fully populated stroke descriptor to the list 
                // I will use it later to activate the strokes sequentuially
                strokes.Add(s);
            }
        }

        foreach(Stroke s in strokes)
        {
            s.gameObject.SetActive(false);
        }

        if(strokes.Count > 0)
        {
            StartStroke(0);
        }
    }

    public void StartStroke(int index)
    {
        if(index>0 && (index -1) < strokes.Count)
        {
            strokes[index-1].gameObject.SetActive(false) ;
        }

        // if we finished the last stroke, clean up and hand control back to the spawner
        if (index >= strokes.Count)
        {
            //Debug.Log("All strokes complete!");
            if(currentMarker != null)
            {
                Destroy(currentMarker);
            }

            if(currentLine != null)
            {
                Destroy(currentLine.gameObject);
            }

            if (LetterSpawner != null)
            {
                LetterSpawner.OnLetterComplete();
            }
            return;
        }

        Stroke current = strokes[index];
        current.gameObject.SetActive(true);

        currentStroke = current.collider;
        startPoint = current.startPoint;
       
        // wire the stroke completion trigger to call back into this manager
        endPoint = current.endPoint;
        EndPointCheck trigger = endPoint.GetComponent<EndPointCheck>();
        if (trigger != null)
        {
            trigger.traceManager = this;
        }


        if (currentMarker!=null)
        {
            Destroy(currentMarker);
        }

        // spawn marker at the strokes start. push z pos slightly forward so that always touches the marker first
        // and not the underlying stroke collider
        Vector3 spawnPos = startPoint.position;
        spawnPos.z = -1.0f;  // avoid pressing the stroke instead of marker
        currentMarker = Instantiate(markerPrefab, spawnPos, Quaternion.identity);
        
        MarkerController controller = currentMarker.GetComponent<MarkerController>();
        if(controller!= null)
        {
            controller.SetStrokeCollider(currentStroke);
            controller.SetStrokePreview(current.StrokePreview);
        }

        // allocate a fresh line for the stroke
        GameObject newLine = Instantiate(linePrefab);
        newLine.transform.position = new Vector3(0, 0, 0.5f);  
        currentLine = newLine.GetComponent<LineRenderer>();
        currentLine.sortingLayerName = "Default";
        currentLine.sortingOrder = 3;
        currentLine.startWidth = 0.75f;
        currentLine.endWidth = 0.75f;
        currentLine.numCapVertices = 10;
        currentLine.positionCount = 0;
        points.Clear();

        drawnLines.Add(newLine);
    }

    public void OnStrokeComplete()
    {
       
        if (advancingStroke)
        {
            return;
        }

        advancingStroke = true;

        if (wordSFX)
        {
            wordSFX.PlayStrokeComplete();
        }
        
        // spawm celebratory VFX at endpoint
        if (sparklePrefab)
        {
            Vector3 spawnAt = endPoint ? endPoint.position : currentMarker.transform.position;
            Instantiate(sparklePrefab, spawnAt, Quaternion.identity);
        }

        StartCoroutine(AdvanceAfterDelay());
    }

    private System.Collections.IEnumerator AdvanceAfterDelay()
    {
        yield return new WaitForSeconds(nextStrokeDelay);

        currentStrokeIndex++;
        advancingStroke=false;
        StartStroke(currentStrokeIndex);
    }

    public void ClearDrawing()
    {
        if(currentMarker != null)
        {
            Destroy(currentMarker);
        }

        foreach(GameObject line in drawnLines)
        {
            if (line != null)
            {
                Destroy(line);
            }
        }

        drawnLines.Clear(); 
    }

    void Update()
    {
        // drawing is idel unless we have an active marker or stroke
        if (currentMarker == null || currentStroke == null)
        {
            return;
        }

        // keep the rendered path slightly infront of the letter mesh so the line
        // is visible even if the letter is coplanar
        Vector3 markerPos = currentMarker.transform.position;
        markerPos.z = 0.5f;

        Vector2 marker2D = new Vector2 (markerPos.x, markerPos.y);

        // only record points when the marker is inside the stroke boundry so
        // line is not rendered outside of the letter
        if(currentStroke.IsInside(marker2D))
        {
            // spacing guard, append only if the marker has moved a minimum distance
            // since the last point, reducing vertext count and jaggy overdrawing
            if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], markerPos) > 0.02f) //  Add to list only if it is far enough
            {
                if (currentStroke.IsInside(marker2D))
                {
                    points.Add(markerPos);
                    currentLine.positionCount = points.Count;
                    currentLine.SetPosition(points.Count - 1, markerPos);
                }
            }
        }
        

       
    }
}
