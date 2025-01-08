using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ScoreManager
{
    private static float currentScore;
    // Start is called before the first frame update
    public static void SetScore(float score) 
    {
        currentScore = score;
    }

    public static float GetScore() 
    {
        return currentScore;
    }

}
