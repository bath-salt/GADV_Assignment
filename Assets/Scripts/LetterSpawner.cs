using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public GameObject[] letterPrefabs;
    public TraceManager TraceManager;
    public Transform spawnLocation;

    private GameObject currentLetter;

    private string currentWord = "CAT";
    private int currentLetterIndex = 0;

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
            Debug.Log("WORD COMPLETE");
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

    void Start()
    {
        StartWord(GameSession.SelectedWord);  //  CHANGE LATER FOR DYNAMIC LOADING
    }
}
