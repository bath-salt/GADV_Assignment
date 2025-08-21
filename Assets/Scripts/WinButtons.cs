using UnityEngine;
using UnityEngine.SceneManagement;

public class WinButtons : MonoBehaviour
{
    public enum Action {PlayAgain, Title}
    public Action action;

    void Update()
    {
        // skips checks if this button isnt active in the hierarchy
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        // onlyn handle the first active touch, and only at the "Bega" phase
        // prevents multiple triggers while finger is held"
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;    

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D hit = Physics2D.OverlapPoint(worldPos);

            // trigger only if this exact button's collider was tapped
            if (hit != null && hit.gameObject == gameObject)
            {
                switch(action)
                {
                    case Action.PlayAgain:
                        PlayAgain();
                        break;

                    case Action.Title:
                        GoToTitle(); 
                        break;
                }
            } 
        }
    }
    public void PlayAgain()
    {
        GameSession.Reset();
        SceneManager.LoadScene("MonsterRequests");
    }
    public void GoToTitle()
    {
        GameSession.Reset();
        SceneManager.LoadScene("Main Menu");
    }
}
