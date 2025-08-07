using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public SpriteRenderer requestRenderer;
    public GameObject feedbackPanel;
    public TextMeshPro feedbackText; 
    public MonsterRequest monsterRequest;
    public void ShowMonsterRequest(Sprite image)
    {
        requestRenderer.sprite = image;   
        requestRenderer.gameObject.SetActive(true);
        feedbackPanel.SetActive(false);
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
}
