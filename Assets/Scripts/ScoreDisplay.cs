using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshPro scoreText;
    private int lastShown = -1; // first update always refreshes the text
    void Update()
    {
        if (scoreText == null)
        {
            return;
        }

        // only update the label when the score has changed
        if(lastShown != GameSession.Score)
        {
            lastShown = GameSession.Score;  
            scoreText.text = $"ចំណុច: {GameSession.Score}/{GameSession.TargetScoreToWin}";
        }
    }
}
