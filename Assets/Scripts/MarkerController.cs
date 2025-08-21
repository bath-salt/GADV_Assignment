using UnityEngine;

public class MarkerController : MonoBehaviour
{
    // drag controller for the marker. 
    // hides stroke based on last marker movement
    private StrokeCollider strokeCollider;

    private GameObject strokePreview;
    private Camera cam;
    private Vector3 dragOffset = Vector3.zero;

    private bool isDragging = false;
    private bool hasMoved = false;
    private float lastMoveTime = 0f;
    private float idleThreshhold = 5f;

    void Start()
    {
        // cache the camera once which reduces per-frame camera.main lookups in update
        cam = Camera.main;
    }

    public void SetStrokePreview(GameObject preview)
    {
        strokePreview = preview;
        if (strokePreview != null)
        {
            strokePreview.SetActive(true);  // always start visible
        }
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

            if (touch.phase == TouchPhase.Began)
            {
                // only begin dragging if the player actually touched the marker
                // prevents snapping the marker from afar when the player taps elsewhere
                Collider2D hit = Physics2D.OverlapPoint(touch2D);
                if(hit != null)
                {
                    Debug.Log($"Touched: {hit.name}");
                }
                else
                {
                    Debug.Log("hit nothing");
                }
                if (hit != null && hit.transform == transform)
                {
                    // preserves the grab offset so the marker follows from the contact point
                    // the player touched no the marker's center
                    isDragging = true;
                    dragOffset = transform.position - touchWorldPos;
                }

            }

            if (isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                Vector3 targetPosition = touchWorldPos + dragOffset;
                if (strokeCollider != null)
                {
                    // require the touch to be inside the stroke and have a small margin available 
                    // in all directions. The margin ("buffer") avoids edge flicker
                    // where tiny jitters would push the marker across the collider boundry 
                    // ensuring that players cannot drag the marker out of the boundry
                    Vector2 target2D = new Vector2(targetPosition.x, targetPosition.y);
                    float buffer = 0.2f;
                    bool isInside = strokeCollider.IsInside(touch2D);
                    bool hasRoom = strokeCollider.IsInside(new Vector2(touch2D.x + buffer, touch2D.y)) &&
                       strokeCollider.IsInside(new Vector2(touch2D.x - buffer, touch2D.y)) &&
                       strokeCollider.IsInside(new Vector2(touch2D.x, touch2D.y + buffer)) &&
                       strokeCollider.IsInside(new Vector2(touch2D.x, touch2D.y - buffer));

                    if (isInside && hasRoom)
                    {
                        // If player drags their finger out of the boundry
                        // but re-enters at a different point in the letter, lerp to ensuree that
                        // the marker is not teleporting to the players finger when re-entering
                        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);  
                        lastMoveTime = Time.time;


                        // hide the preview when the player starts drawing
                        if (strokePreview != null && strokePreview.activeSelf)
                        {
                            strokePreview.SetActive(false);
                            Debug.Log("hiding preview");
                        }
                        hasMoved = true;
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }

        }

        // bring back preview if idle for too long
        if (hasMoved && strokePreview != null && !strokePreview.activeSelf)
        {
            if (Time.time - lastMoveTime >= idleThreshhold)
            {
                strokePreview.SetActive(true);
                hasMoved = false;
            }
        }
    }
}