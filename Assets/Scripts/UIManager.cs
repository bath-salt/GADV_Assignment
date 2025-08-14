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
        feedbackText.text = isCorrect ? "នោះ​គឺ​ត្រឹមត្រូវ!" : "ឱ! ព្យាយាមម្តងទៀត";
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

    public void HideSpeech()
    {
        if(requestBubble != null) requestBubble.SetActive(false);
        if(speechText != null) speechText.gameObject.SetActive(false);
    }
}
