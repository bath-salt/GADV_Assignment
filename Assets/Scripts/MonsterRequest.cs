using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MonsterRequest : MonoBehaviour
{
    [System.Serializable]
    public class WordRequest
    {
        public string words;                    
        public string category;
        public GameObject requestPrefab;
    }

    public List<WordRequest> wordRequests = new List<WordRequest>();
    private WordRequest currentRequest;

    public UIManager uimanager;
    public MonsterRequestSFX monsterRequestSFX;

    public void GiveRandomRequest()
    {
        // pick one request at randome
        currentRequest = wordRequests[Random.Range(0, wordRequests.Count)];
        uimanager.ShowMonsterRequest(currentRequest.requestPrefab);
    }

    public void OnCategoryChosen(string selectedCategory)
    {
        // check if category chosen is correct and the loads the tracing scene with the correct word
        // shows player character's thought bubble saying thats correct in khmer
        if (selectedCategory == currentRequest.category)
        {
            // Debug.Log("Correct word is in the right category" + currentRequest.category);
            monsterRequestSFX?.PlayCorrect();
            GameSession.SelectedWord = currentRequest.words;
            GameSession.SelectedWordPrefab = currentRequest.requestPrefab;
            uimanager.ShowFeedback(true);

            Invoke(nameof(LoadTracingScene), 1f);
        }
        else
        {
            // if wrong, play a different sfx and show different thought bubble message
            monsterRequestSFX?.PlayWrong();
            // Debug.Log("Incorrect");
            uimanager.ShowFeedback(false);
        }
    }

    void LoadTracingScene()
    {
        SceneManager.LoadScene("tracing");
    }

}
