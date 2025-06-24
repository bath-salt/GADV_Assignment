using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
