using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TracingUIManager : MonoBehaviour
{
    public GameObject completionPanel;
    public TextMeshPro english;
    public TextMeshPro khmer;
    public TextMeshPro goodJob;

    public void ShowCompletionPanel(string englishWord, string khmerTranslation)
    {
        if (completionPanel)
        {
            completionPanel.SetActive(true);
        }

        if (goodJob)
        {
            goodJob.text = "ការងារល្អ។!";
        }

        if (english)
        {
            english.text = englishWord;
        }

        if (khmer)
        {
            khmer.text = khmerTranslation;
        }
    }

    // case- insensitive look up table
    // OrdinalIgnoreCase ensures consistentmatching regardless of user input casing
    // (e.g, "cat", "Cat", "CAT")
    private Dictionary<string, string> khmerTranslations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"Cat", "ឆ្មា"},
        {"Dog", "ឆ្កែ"},
        {"Cow", "គោ" },
        {"Apple", "ផ្លែប៉ោម" }
    };

    public string GetKhmerTranslation(string englishWord)
    {
        if (string.IsNullOrWhiteSpace(englishWord))
        {
            return "";
        }

        // trim to tolerate incidental whitespace from upstream UI, case insensitive comparer
        // handles the rest and returns "" on miss keeping the UI clean (no placeholder)
        if (khmerTranslations.TryGetValue(englishWord.Trim(), out var km))
        {
            return km;
        }
            
        return "";
    }

}
