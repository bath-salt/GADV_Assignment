using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterSpawner : MonoBehaviour
{
    // spawns letter for given word
    // on finish, triggers SFX and UI and updates session state, returns to 
    // monster request scene after a short delay
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
        // Normalise everything to uppercase to ensure that my
        // letter to index math stays consistent with the 
        // letterPrefabs array ordering. 
        currentWord = word.ToUpper();
        currentLetterIndex = 0;
        LoadNextLetter();
    }
    
    public void LoadNextLetter()
    {
        // Ensure that no left over drawing or letters before loading a new letter
        if (currentLetter != null)
        {
            Destroy(currentLetter);
        }

        if(TraceManager != null)
        {
            TraceManager.ClearDrawing();
        }

        // Check if the word is completed 
        if (currentLetterIndex >= currentWord.Length)
        {
            if(!wordCompleted)
            {
                wordCompleted = true;

                // play the sfx if assigned but dont crash if it is missing
                wordSFX?.PlayWordComplete();

                if(TracingDisplay != null)
                {
                    TracingDisplay.ClearDisplay();
                }

                GameSession.Score += 1;

                // pull translation for the summary UI, keeps localisation in UI layer
                // Shows goojob, khmer translation and then the english word after tracing
                // the whole word
                var km = TracingUIManager.GetKhmerTranslation(GameSession.SelectedWord);
                
                TracingUIManager.ShowCompletionPanel(GameSession.SelectedWord, km);

                GameSession.JustDelivered = true;

                // pause to allow players to read the completion panel before swithcing scenes
                Invoke(nameof(ReturnToMonsterScene), 3f);
            }
            return;
        }

        // Map ASCII letter to array index ('A' -> 0, 'B' -> 1 etc)
        char letter = currentWord[currentLetterIndex];
        int index = letter - 'A';

        if (index >= 0 && index < letterPrefabs.Length)
        {
            // spawn the visual for the current letter  and let tracemanager
            // set up colliders and marker for tracing. 
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
        // called by tracemanager when a letter's strokes are all completed
        // advance the pointer and attempt to spawn the next letter/finish
        currentLetterIndex++;
        LoadNextLetter() ;
    }

    private void ReturnToMonsterScene()
    {
        SceneManager.LoadScene("MonsterRequests");
    }
    void Start()
    {
        StartWord(GameSession.SelectedWord); 
    }
}
