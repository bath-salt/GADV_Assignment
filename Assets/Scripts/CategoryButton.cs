using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    public string CategoryName;
    public MonsterRequest monsterRequest;

    private void Update()
    {
        // Ignore input if a global lock is active (e.g when win screen is showing, I do not want players accidentally pressing these buttons)
        if (GameSession.InputLocked)
        {
            return;
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // convrets the screen touch position into world space,
                // because Physics2D.overlapPoint works in world space coordinaes
                // I then check if the touch landed onn any colliders in the scene,
                // determining whether the tap interacts with something. 
                Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hit = Physics2D.OverlapPoint(touchWorldPos);

                if (hit != null)
                {
                    Debug.Log("Touched" + hit.name);
                }
                else
                {
                    Debug.Log("Hit nothing");
                }

                // Only responds if the tap was on this specific button, not just any collider
                if (hit != null && hit.gameObject == gameObject)
                {
                    Debug.Log($"Tapped: {CategoryName}");
                    monsterRequest.OnCategoryChosen(CategoryName);
                }
            }
        }
    }
}
