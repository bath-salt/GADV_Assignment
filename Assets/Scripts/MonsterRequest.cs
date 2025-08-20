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
    void Start()
    {
    }

    public void GiveRandomRequest()
    {
        currentRequest = wordRequests[Random.Range(0, wordRequests.Count)];
        uimanager.ShowMonsterRequest(currentRequest.requestPrefab);
    }

    public void OnCategoryChosen(string selectedCategory)
    {
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
