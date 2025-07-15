using UnityEngine;
using System.Collections.Generic;
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
    // private bool isDrawing = false; 
    public void SetupLetter(GameObject letter)
    {
        Transform stroke1 = letter.transform.Find("Stroke1");
        currentStroke = stroke1.GetComponent<StrokeCollider>();
        if (stroke1 != null )
        {
            startPoint = stroke1.Find("StartPoint");
            endPoint = stroke1.Find("EndPoint");

            if (startPoint != null)
            {
                if(currentMarker != null)
                {
                    Destroy(currentMarker);  //Destroy any existing markers
                }

                currentMarker = Instantiate(markerPrefab, startPoint.position, Quaternion.identity);  //Create new marker at startpoint

                MarkerController controller = currentMarker.GetComponent<MarkerController>();
                if (controller != null )
                {
                    controller.SetStrokeCollider(currentStroke);
                    Transform preview = stroke1.Find("StrokePreview");
                    if (preview != null)
                    {
                        controller.SetStrokePreview(preview.gameObject);
                    }
                }

                GameObject newLine = Instantiate(linePrefab);
                currentLine = newLine.GetComponent<LineRenderer>();
                currentLine.sortingLayerName = "Default";
                currentLine.sortingOrder = 5;
                currentLine.positionCount = 0;
                points.Clear();
            }
            else
            {
                Debug.Log("StartPoint not found.");
            }

            if (endPoint == null)
            {
                Debug.Log("EndPoint not found");
            }
        }
        else
        {
            Debug.Log("Stroke1 not found in letter prefab");
        }
    }

    void Update()
    {
        if (currentMarker == null || currentStroke == null) return; 
        Vector3 markerPos = currentMarker.transform.position;
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
        else
        {
            // isDrawing = false;
        }

        if (Vector3.Distance(markerPos, endPoint.position) < 0.3f)
        {
            Debug.Log("stroke Complete!"); 

            // isDrawing = false;
        }
    }
}
