using System.Collections.Generic;
using UnityEngine;

public static class GameSession
{
    public static string SelectedWord;
    public static GameObject SelectedWordPrefab;

    public static bool JustDelivered = false;


    public static int Score = 0;

    public static void Reset()
    {
        Score = 0;
        SelectedWord = null;
        SelectedWordPrefab = null;
    }


}