using UnityEngine;
using UnityEngine.SceneManagement;

public class WinButtons : MonoBehaviour
{
    public enum Action {PlayAgain, Title}
    public Action action;

    void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;    

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D hit = Physics2D.OverlapPoint(worldPos);

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
        SceneManager.LoadScene("Monster Requests");
    }
    public void GoToTitle()
    {
        GameSession.Reset();
        SceneManager.LoadScene("Main Menu");
    }
}
