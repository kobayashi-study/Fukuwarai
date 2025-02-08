using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : ButtonController
{
    [SerializeField] private BGMController bgmController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override IEnumerator SceneChange()
    {
        if (bgmController != null)
        {
            bgmController.SetDontDestroyFlg(false);
            bgmController.DestroyBGM();
        }

        BGMController[] bgmControllers = GameObject.FindObjectsOfType<BGMController>();

        foreach (BGMController bgm in bgmControllers)
        {
            bgm.SetDontDestroyFlg(false);
            bgm.DestroyBGM();
        }

        GameManager.SetBGM(false);
        yield return base.SceneChange();
    }
}
