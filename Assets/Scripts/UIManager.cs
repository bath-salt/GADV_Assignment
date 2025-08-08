using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameObject currentRequestImage;
    public GameObject requestBubble;
    public GameObject feedbackPanel;
    public TextMeshPro feedbackText; 
    public MonsterRequest monsterRequest;
    public TextMeshPro speechText;
    public void ShowMonsterRequest(GameObject prefabToSpawn)
    {
        if(currentRequestImage != null)
        {
            Destroy(currentRequestImage);
        }

        currentRequestImage = Instantiate(prefabToSpawn);
        requestBubble.SetActive(true);
    }  
    
    public void ShowFeedback(bool isCorrect)
    {
        feedbackPanel.SetActive(true);
        feedbackText.text = isCorrect ? "Correct! Let's Begin tracing!" : "Oops! try again";
    }

    public void OnCategoryButtonClicked(string category)
    {
        monsterRequest.OnCategoryChosen(category);
    }

    public void ShowSpeech(string message)
    {
        if (requestBubble != null) requestBubble.SetActive(true);
        if (speechText != null) speechText.text = message;
    }
}
