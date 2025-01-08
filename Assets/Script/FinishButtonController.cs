using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public class FinishButtonController : ButtonController
{
    private ScoreController scoreController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        scoreController = GameObject.Find("GameManager").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override IEnumerator SceneChange() 
    {
        ScoreManager.SetScore(scoreController.CalculateScore());
        yield return base.SceneChange();
    }
}
