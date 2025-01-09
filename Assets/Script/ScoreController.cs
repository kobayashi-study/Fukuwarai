using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private Vector3 rightEyebrowA = new Vector3(-5.68f, 0.954f, 0);
    private Vector3 leftEyebrowA = new Vector3(-4.27f, 0.954f, 0);
    private Vector3 rightEyeA = new Vector3(-6.33f, -0.236f, 0);
    private Vector3 leftEyeA = new Vector3(-3.617f, -0.236f, 0);
    private Vector3 noseA = new Vector3(-4.96f, -1.06f, 0);
    private Vector3 rightCheekA = new Vector3(-6.95f, -1.243f, 0);
    private Vector3 leftCheekA = new Vector3(-2.997f, -1.243f, 0);
    private Vector3 mouthA = new Vector3(-4.978f, -1.872f, 0);

    private List<GameObject> positions = new List<GameObject>();
    private List<Vector3> positionsA = new List<Vector3>();
    private float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        positions.Add(GameObject.Find("Okame_Eyebrow1"));
        positions.Add(GameObject.Find("Okame_Eyebrow2"));
        positions.Add(GameObject.Find("Okame_RightEye"));
        positions.Add(GameObject.Find("Okame_LeftEye"));
        positions.Add(GameObject.Find("Okame_Nose"));
        positions.Add(GameObject.Find("Okame_Cheek1"));
        positions.Add(GameObject.Find("Okame_Cheek2"));
        positions.Add(GameObject.Find("Okame_Mouth"));

        positionsA.Add(rightEyebrowA);
        positionsA.Add(leftEyebrowA);
        positionsA.Add(rightEyeA);
        positionsA.Add(leftEyeA);
        positionsA.Add(noseA);
        positionsA.Add(rightCheekA);
        positionsA.Add(leftCheekA);
        positionsA.Add(mouthA);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateScore() 
    {
        for(int i = 0; i <positions.Count; i++) 
        {
            float distance = Vector3.Distance(positions[i].transform.localPosition, positionsA[i]);
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
        }
        return score;
    }
}
