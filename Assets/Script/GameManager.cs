using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
    private static float currentScore;
    private static bool BGM = false;
    // Start is called before the first frame update
    public static void SetScore(float score)
    {
        currentScore = score;
    }

    public static float GetScore()
    {
        return currentScore;
    }

    public static void SetBGM(bool flag)
    {
        BGM = flag;
    }

    public static bool GetBGM()
    {
        return BGM;
    }
}