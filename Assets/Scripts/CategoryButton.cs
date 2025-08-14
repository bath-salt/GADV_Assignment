using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    public string CategoryName;
    public MonsterRequest monsterRequest;

    private void Update()
    {
        if (GameSession.InputLocked) return;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
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

                if (hit != null && hit.gameObject == gameObject)
                {
                    Debug.Log($"Tapped: {CategoryName}");
                    monsterRequest.OnCategoryChosen(CategoryName);
                }
            }
        }
    }
}
