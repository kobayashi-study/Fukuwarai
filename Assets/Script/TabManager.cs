using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public static TabManager Instance { get; private set; }
    private bool dontTouch = false;
    private bool isMove = false;
    private GameObject currentTopTab;
    private SpriteRenderer currentSpriteRender;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTabEnter(GameObject tab, SpriteRenderer spriteRenderer)
    {
        if (currentTopTab != null && currentTopTab != tab)
        {
            currentSpriteRender.sortingOrder = 7;
        }

        spriteRenderer.sortingOrder = 8;
        currentTopTab = tab;
        currentSpriteRender = spriteRenderer;
    }

    public void OnTabExit(GameObject tab, GameObject hitObject)
    {
        if (hitObject != null && hitObject.tag == "Tab")
        {
            return;
        }
    }
    public bool getDontTouch() 
    {
        return dontTouch;
    }

    public void setDontTouch(bool flg) 
    {
        dontTouch = flg;
    }

    public bool getIsMove()
    {
        return isMove;
    }

    public void setIsMove(bool flg)
    {
        isMove = flg;
    }

    public GameObject getCurrentTopTab()
    {
        return currentTopTab;
    }
}