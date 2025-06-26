using System.Collections;
using UnityEngine;

public class LetterTest : MonoBehaviour
{
    public LetterManager letterManager;
    void Start()
    {
        StartCoroutine(CycleAlphabet());
    }

    IEnumerator CycleAlphabet()
    {
        for(char c = 'A'; c <= 'Z'; c++)
        {
            letterManager.showLetter(c);
            Debug.Log($"Showing letter: {c}");
            yield return new WaitForSeconds(3f);
        }
    }

}
