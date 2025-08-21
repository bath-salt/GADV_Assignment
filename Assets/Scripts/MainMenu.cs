using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject difficultyPanel;

    void Awake()
    {
        // awake ensures downstream code (and intitial UI) can see the right value
        GameSession.LoadDifficultyOrDefault();
        ShowMain();
    }

    public void OnPlayPressed()
    {
        // swap to the difficulty selection screen
        if(mainMenuPanel) mainMenuPanel.SetActive(false);
        if(difficultyPanel) difficultyPanel.SetActive(true);
    }

    public void OnBackPressed()
    {
        ShowMain();
    }

    // button handlers delegate to a single start path to keep behavior consistent
    public void OnEasyPressed() => StartGame(GameSession.Difficulty.Easy);
    public void OnMediumPressed() => StartGame(GameSession.Difficulty.Medium);
    public void OnHardPressed() => StartGame(GameSession.Difficulty.Hard);

    private void StartGame(GameSession.Difficulty d)
    {
        // centralised entry point, set difficulty and clear prior session state
        // after doing so can move on to next scene
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




