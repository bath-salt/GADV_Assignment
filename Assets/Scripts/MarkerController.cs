using UnityEngine;

public class MarkerController : MonoBehaviour
{
    private StrokeCollider strokeCollider;
    private Camera cam;
    private Vector3 dragOffset = Vector3.zero;

    private bool isDragging = false;

    void Start()
    {
        cam = Camera.main;
    }

    public void SetStrokeCollider(StrokeCollider stroke)
    {
        strokeCollider = stroke;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchWorldPos = cam.ScreenToWorldPoint(touch.position);
            touchWorldPos.z = 0f;

            Vector2 touch2D = new Vector2(touchWorldPos.x, touchWorldPos.y);

            if(touch.phase == TouchPhase.Began)
            {
                Collider2D hit = Physics2D.OverlapPoint(touch2D);
                if (hit != null && hit.transform == transform)
                {
                    isDragging = true;
                    dragOffset = transform.position - touchWorldPos;
                }
                
            }

            if(isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                Vector3 targetPosition = touchWorldPos + dragOffset;
                if(strokeCollider !=null)
                {
                    Vector2 target2D = new Vector2(targetPosition.x, targetPosition.y);
                    float buffer = 0.2f;
                    bool isInside = strokeCollider.IsInside(touch2D);
                    bool hasRoom = strokeCollider.IsInside(new Vector2(touch2D.x + buffer, touch2D.y)) &&
                       strokeCollider.IsInside(new Vector2(touch2D.x - buffer, touch2D.y)) &&
                       strokeCollider.IsInside(new Vector2(touch2D.x, touch2D.y + buffer)) &&
                       strokeCollider.IsInside(new Vector2(touch2D.x, touch2D.y - buffer));
                    if(isInside && hasRoom)
                    {
                        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);  // so marker does not teleport when you drag out of bounds and back in again
                        Debug.Log("Marker Moved");
                    }
                }
            }

            if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }    
    }
}
