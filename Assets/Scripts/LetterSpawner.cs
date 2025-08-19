using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterSpawner : MonoBehaviour
{
    public GameObject[] letterPrefabs;
    public TraceManager TraceManager;
    public TracingUIManager TracingUIManager;
    public WordSFX wordSFX;
    public Transform spawnLocation;
    public TracingDisplay TracingDisplay;
    private GameObject currentLetter;

    private string currentWord = "CAT";
    private int currentLetterIndex = 0;
    private bool wordCompleted = false;

    public void StartWord(string word)
    {
        currentWord = word.ToUpper();
        currentLetterIndex = 0;
        LoadNextLetter();
    }
    
    public void LoadNextLetter()
    {
        if (currentLetter != null)
        {
            Destroy(currentLetter);
        }

        if(TraceManager != null)
        {
            TraceManager.ClearDrawing();
        }

        if (currentLetterIndex >= currentWord.Length)
        {
            if(!wordCompleted)
            {
                wordCompleted = true;
                wordSFX?.PlayWordComplete();

                if(TracingDisplay != null)
                {
                    TracingDisplay.ClearDisplay();
                }

                GameSession.Score += 1;

                var km = TracingUIManager.GetKhmerTranslation(GameSession.SelectedWord);
                
                TracingUIManager.ShowCompletionPanel(GameSession.SelectedWord, km);

                GameSession.JustDelivered = true;
                Invoke(nameof(ReturnToMonsterScene), 3f);
            }
            return;
        }

        char letter = currentWord[currentLetterIndex];
        int index = letter - 'A';

        if (index >= 0 && index < letterPrefabs.Length)
        {
            currentLetter = Instantiate(letterPrefabs[index], spawnLocation.position, Quaternion.identity);
            TraceManager.SetupLetter(currentLetter);
        }
        else
        {
            Debug.Log("Invalid Character in word" +  letter);   
        }
    }

    public void OnLetterComplete()
    {
        currentLetterIndex++;
        LoadNextLetter() ;
    }

    private void ReturnToMonsterScene()
    {
        SceneManager.LoadScene("MonsterRequests");
    }
    void Start()
    {
        StartWord(GameSession.SelectedWord);  //  CHANGE LATER FOR DYNAMIC LOADING
    }
}
