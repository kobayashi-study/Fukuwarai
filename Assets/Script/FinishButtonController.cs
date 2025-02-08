using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonController : ButtonController
{
    private ScoreController scoreController;
    [SerializeField] private BGMController bgmController;
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
        GameManager.SetScore(scoreController.CalculateScore());
        bgmController.SetDontDestroyFlg(false);
        bgmController.DestroyBGM();
        GameManager.SetBGM(false);
        yield return base.SceneChange();
    }
}
