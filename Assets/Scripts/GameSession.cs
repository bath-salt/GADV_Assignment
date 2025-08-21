using System.Collections.Generic;
using UnityEngine;

public static class GameSession
{
    // global state holder for my game
    // stores persistent information like word selected score so 
    // multiple scripts across diff scenes can share context
    public static string SelectedWord;
    public static GameObject SelectedWordPrefab;
    public static bool InputLocked;

    public static bool JustDelivered = false;


    public static int Score = 0;

    // used enum to ensure no invalid values (type safety over raw ints)
    // explicit numeric values to ensure difficulty saves/loads consistently from playerprefs
    public enum Difficulty { Easy = 0, Medium = 1, Hard = 2 }

    // default to medium so the game starts in a valid known state
    public static Difficulty CurrentDifficulty { get; private set; } = Difficulty.Medium;

    public static int TargetScoreToWin { get; private set; } = 4;

    // target sscore is tied to difficulty chosen
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

    // if the player has set a difficulty before, reloat it
    // for my play again feature
    // otherwise defualt to medium
    public static void LoadDifficultyOrDefault()
    {
        if(PlayerPrefs.HasKey("difficulty"))
            SetDifficulty((Difficulty)PlayerPrefs.GetInt("difficulty"));
        else
            SetDifficulty(Difficulty.Medium);
    }

    // called when starting a new session
    // Resets scores and selections so no state leaks over
    public static void Reset()
    {
        Score = 0;
        SelectedWord = null;
        SelectedWordPrefab = null;
        JustDelivered = false;
        InputLocked = false;
    }


}