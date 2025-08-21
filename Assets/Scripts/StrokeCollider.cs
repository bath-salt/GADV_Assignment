using UnityEngine;

public class StrokeCollider : MonoBehaviour
{
    public PolygonCollider2D strokeArea;

    void Awake()
    {
        // if not assgned manually in the inspector, auto fetch the 
        // collider attached to this object
        if (strokeArea == null)
        {
            strokeArea = GetComponent<PolygonCollider2D>();
        }
    }

    public bool IsInside(Vector2 point)
    {
        // returns true only if the collider exits and the point is within its shape
        // used by MarkerController to decide whether dragging input is valid
        return strokeArea != null && strokeArea.OverlapPoint(point);
    }

}
