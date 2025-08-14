using System.Collections.Generic;
using UnityEngine;

public static class GameSession
{
    public static string SelectedWord;
    public static GameObject SelectedWordPrefab;
    public static bool InputLocked;

    public static bool JustDelivered = false;


    public static int Score = 0;

    public enum Difficulty { Easy = 0, Medium = 1, Hard = 2 }
    public static Difficulty CurrentDifficulty { get; private set; } = Difficulty.Medium;
    public static int TargetScoreToWin { get; private set; } = 4;

    public static void SetDifficulty(Difficulty d)
    {
        CurrentDifficulty = d;
        TargetScoreToWin = d switch
        {
            Difficulty.Easy => 2,
            Difficulty.Medium => 4,
            Difficulty.Hard => 6,
            _ => 4
        };

        PlayerPrefs.SetInt("difficulty", (int)d);
        PlayerPrefs.Save();
    }

    public static void LoadDifficultyOrDefault()
    {
        if(PlayerPrefs.HasKey("difficulty"))
            SetDifficulty((Difficulty)PlayerPrefs.GetInt("difficulty"));
        else
            SetDifficulty(Difficulty.Medium);
    }
    public static void Reset()
    {
        Score = 0;
        SelectedWord = null;
        SelectedWordPrefab = null;
        JustDelivered = false;
        InputLocked = false;
    }


}