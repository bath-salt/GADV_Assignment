using UnityEngine;

public class StrokeCollider : MonoBehaviour
{
    public PolygonCollider2D strokeArea;

    void Awake()
    {
        if (strokeArea == null)
        {
            strokeArea = GetComponent<PolygonCollider2D>();
        }
    }

    public bool IsInside(Vector2 point)
    {
        return strokeArea != null && strokeArea.OverlapPoint(point);
    }
}
