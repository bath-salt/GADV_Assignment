using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject difficultyPanel;

    void Awake()
    {
        GameSession.LoadDifficultyOrDefault();
        ShowMain();
    }

    public void OnPlayPressed()
    {
        if(mainMenuPanel) mainMenuPanel.SetActive(false);
        if(difficultyPanel) difficultyPanel.SetActive(true);
    }

    public void OnBackPressed()
    {
        ShowMain();
    }

    public void OnEasyPressed() => StartGame(GameSession.Difficulty.Easy);
    public void OnMediumPressed() => StartGame(GameSession.Difficulty.Medium);
    public void OnHardPressed() => StartGame(GameSession.Difficulty.Hard);

    private void StartGame(GameSession.Difficulty d)
    {
        GameSession.SetDifficulty(d);
        GameSession.Reset();
        SceneManager.LoadScene("MonsterRequests");
    }

    private void ShowMain()
    {
        if(difficultyPanel) difficultyPanel.SetActive(false);
        if (mainMenuPanel) mainMenuPanel.SetActive(true);
    }
}




