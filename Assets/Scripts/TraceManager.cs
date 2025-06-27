using UnityEngine;

public class TraceManager : MonoBehaviour
{
    public GameObject markerPrefab;

    private GameObject currentMarker;
    private Transform startPoint;
    private Transform endPoint;

    public void SetupLetter(GameObject letter)
    {
        Transform stroke1 = letter.transform.Find("Stroke1");

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
        if (currentMarker != null && endPoint != null)
        {
            float distance = Vector3.Distance(currentMarker.transform.position, endPoint.position);
            if (distance < 0.3f)  //Check if marker has reached the end point
            {
                Debug.Log("Stroke complete!");  //Add more functionality maybe create a loop to get through other strokes
            }
        } 
    }
}
