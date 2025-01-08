using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    //顔のパーツを管理する場合、リスト？ディクショナリー？どれで管理が最適か？
    private Vector3 rightEyebrowA = new Vector3(-5.43f, -1.54f, 0);
    private Vector3 leftEyebrowA = new Vector3(-5.43f, -1.54f, 0);
    private Vector3 rightEyeA = new Vector3(-6.77f, 0.1f, 0);
    private Vector3 leftEyeA = new Vector3(-4.07f, 0.1f, 0);
    private Vector3 noseA = new Vector3(-5.43f, -0.72f, 0);
    private Vector3 rightcheekA = new Vector3(-5.43f, -1.54f, 0);
    private Vector3 leftcheekA = new Vector3(-5.43f, -1.54f, 0);
    private Vector3 mouthA = new Vector3(-5.43f, -1.54f, 0);

    private GameObject rightEye;
    private float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        rightEye = GameObject.Find("Okame_RightEye");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateScore() 
    {
        float distance = Vector3.Distance(rightEye.transform.localPosition, rightEyeA);
        Debug.Log(distance);
        if (distance <= 0.5)
        {
            score += 10;
        }
        else if (distance <= 1)
        {
            score += 5;
        }
        else
        {
            score += 0;
        }
        return score;
    }
}
