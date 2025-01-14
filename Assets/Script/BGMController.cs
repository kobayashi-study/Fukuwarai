using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGMController : MonoBehaviour
{
    private bool dontDestroyFlg = true;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (GameManager.GetBGM())
        {
            Destroy(this.gameObject);
            return;
        } 
        else
        {
            if (source.clip.name == "kadomatsu") 
            {
                source.time = 1.5f;
            }
            source.Play();
            GameManager.SetBGM(true);
        }

        if (dontDestroyFlg) 
        {
            DontDestroyOnLoad(this.gameObject);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDontDestroyFlg(bool flg) 
    { 
        this.dontDestroyFlg = flg;
    }

    public void StopBGM() 
    {
        if (source != null) 
        {
            source.Stop();
        }
    }

    public void DestroyBGM() 
    {
        if (dontDestroyFlg == false)
        {
            StopBGM();
            Destroy(this.gameObject);
        }
    }
}
