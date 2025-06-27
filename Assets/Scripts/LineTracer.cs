using UnityEngine;
using System.Collections.Generic;

public class LineTracer : MonoBehaviour
{
    //public Transform marker;
    //public GameObject linePrefab;

    //private Camera cam;
    //private List<Vector3> points = new List<Vector3>();
    //private List<LineRenderer> allLines = new List<LineRenderer>();
    //private LineRenderer currentLine;

    //public float minMoveDistance = 0.02f;
   
    
    //private bool isDragging = false;
    //void Start()
    //{
    //    cam = Camera.main;
    //}

    //void Update()
    //{
    //    if (Input.touchCount > 0)   //check if screen is being touched
    //    {
    //        Touch touch = Input.GetTouch(0);  //store the first touch
    //        Vector3 worldTouch = cam.ScreenToWorldPoint(touch.position);   //touch position is in screen space (pixel coor) but I want world space
    //        Vector2 touch2D = new Vector2(worldTouch.x, worldTouch.y);
    //        Vector3 drawPos = new Vector3(touch2D.x, touch2D.y, 0f);

    //        if (touch.phase == TouchPhase.Began) 
    //        {
    //            Collider2D hit = Physics2D.OverlapPoint(touch2D);
    //            if (hit != null && hit.transform == marker)
    //            {
    //                isDragging = true;
                    
    //                GameObject newLineObj = Instantiate(linePrefab);
    //                currentLine = newLineObj.GetComponent<LineRenderer>();
    //                currentLine.positionCount = 0;
    //                currentLine.widthMultiplier = 0.5f;

    //                points.Clear();
    //                allLines.Add(currentLine);
    //            }   
    //        }

    //        if (isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
    //        {
    //            marker.position = drawPos;

    //            if(points.Count == 0 || Vector3.Distance(points[points.Count -1], drawPos) > minMoveDistance)
    //            {
    //                points.Add(drawPos);
    //                currentLine.positionCount = points.Count;
    //                currentLine.SetPosition(points.Count -1, drawPos);
    //            }
    //        }
            
    //        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
    //        {
    //            isDragging = false;
    //        }
    //    }
    //}
}
