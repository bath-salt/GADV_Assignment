using UnityEngine;

public class EndPointCheck : MonoBehaviour
{
    public TraceManager traceManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player's marker reaches the endpoint collider
        // inform traemanager that the current stroke is complete
        if (other.CompareTag("Marker"))
        {
            traceManager.OnStrokeComplete();
        }
    }
}
