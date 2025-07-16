using UnityEngine;

public class EndPointCheck : MonoBehaviour
{
    public TraceManager traceManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Marker"))
        {
            Debug.Log("TEST1");
            traceManager.OnStrokeComplete();
        }
    }
}
