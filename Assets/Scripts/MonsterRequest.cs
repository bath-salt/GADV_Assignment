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
        public Sprite image;
    }

    public List<WordRequest> wordRequests = new List<WordRequest>();
    private WordRequest currentRequest;

    public UIManager uimanager;
    void Start()
    {
        GiveRandomRequest();
    }

    public void GiveRandomRequest()
    {
        currentRequest = wordRequests[Random.Range(0, wordRequests.Count)];
        uimanager.ShowMonsterRequest(currentRequest.image);
    }

    public void OnCategoryChosen(string selectedCategory)
    {
        if (selectedCategory == currentRequest.category)
        {
            Debug.Log("Correct word is in the right category" + currentRequest.category);
            GameSession.SelectedWord = currentRequest.words;
            uimanager.ShowFeedback(true);

            Invoke(nameof(LoadTracingScene), 1f);
        }
        else
        {
            Debug.Log("Incorrect");
            uimanager.ShowFeedback(false);
        }
    }

    void LoadTracingScene()
    {
        SceneManager.LoadScene("tracing");
    }

}
