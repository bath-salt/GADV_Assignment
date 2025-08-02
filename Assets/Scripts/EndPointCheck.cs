using UnityEngine;

public class EndPointCheck : MonoBehaviour
{
    public TraceManager traceManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Marker"))
        {
            traceManager.OnStrokeComplete();
        }
    }
}
