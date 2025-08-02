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
    public GameObject markerPrefab;
    public GameObject linePrefab;

    private GameObject currentMarker;
    private LineRenderer currentLine;
    private StrokeCollider currentStroke;
    private Transform startPoint;
    private Transform endPoint;

    private List<Vector3> points = new List<Vector3>();

    private List<Stroke> strokes = new List<Stroke>();
    private int currentStrokeIndex = 0;
    // private bool isDrawing = false; 
    public void SetupLetter(GameObject letter)
    {
        strokes.Clear();  //Clear list from previous letters
        currentStrokeIndex = 0;

        foreach(Transform child in letter.transform)
        {
            if (child.name.StartsWith("Stroke"))
            {
                Stroke s = new Stroke();
                s.gameObject = child.gameObject;
                s.startPoint = child.Find("StartPoint");
                s.endPoint = child.Find("EndPoint");
                s.collider = child.GetComponent<StrokeCollider>();
                if (s.collider == null)
                {
                    Debug.LogError("Collider not found in " + child.name);
                }
                s.StrokePreview = child.Find("StrokePreview")?.gameObject;
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

    private void StartStroke(int index)
    {
        if(index>0 && index -1 < strokes.Count)
        {
            strokes[index-1].gameObject.SetActive(false) ;
        }
        if (index >= strokes.Count)
        {
            Debug.Log("All strokes complete!");
            return;
        }

        Stroke current = strokes[index];
        current.gameObject.SetActive(true);

        currentStroke = current.collider;
        startPoint = current.startPoint;
       
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

        Vector3 spawnPos = startPoint.position;
        spawnPos.z = -1.0f;  // avoid pressing the stroke instead of marker
        currentMarker = Instantiate(markerPrefab, spawnPos, Quaternion.identity);
        
        MarkerController controller = currentMarker.GetComponent<MarkerController>();
        if(controller!= null)
        {
            controller.SetStrokeCollider(currentStroke);
            controller.SetStrokePreview(current.StrokePreview);
        }

        GameObject newLine = Instantiate(linePrefab);
        newLine.transform.position = new Vector3(0, 0, 0.5f);  // ensure line is below the letter image
        currentLine = newLine.GetComponent<LineRenderer>();
        currentLine.sortingLayerName = "Default";
        currentLine.sortingOrder = 3;
        currentLine.startWidth = 0.75f;
        currentLine.endWidth = 0.75f;
        currentLine.numCapVertices = 10;
        currentLine.positionCount = 0;
        points.Clear();
    }

    public void OnStrokeComplete()
    {
        Debug.Log("Stroke Complete");
        currentStrokeIndex++;
        StartStroke(currentStrokeIndex);
    }

    void Update()
    {
        if (currentMarker == null || currentStroke == null) return; 
        Vector3 markerPos = currentMarker.transform.position;
        markerPos.z = 0.5f;
        Vector2 marker2D = new Vector2 (markerPos.x, markerPos.y);

        if(currentStroke.IsInside(marker2D))
        {
            // isDrawing = true;

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
