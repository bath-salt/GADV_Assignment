using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshPro scoreText;
    private int lastShown = -1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText == null) return;
        if(lastShown != GameSession.Score)
        {
            lastShown = GameSession.Score;  
            scoreText.text = $"ចំណុច: {GameSession.Score}";
        }
    }
}
