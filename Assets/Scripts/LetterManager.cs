using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public UnityEngine.UI.Image tracingLetterImage;
    public Sprite[] letterSprites; 

    public void showLetter(char letter)
    {
        int index = letter - 'A'; //ascii codes if i want to get letter A, it'll be index 065 - 065 = 0 etc
        tracingLetterImage.sprite = letterSprites[index];  // Display the letter I want 
    }
}
