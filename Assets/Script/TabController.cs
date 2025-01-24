using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TabController : MonoBehaviour
{
    private new Camera camera;  
    private SpriteRenderer spriteRenderer;
    private GameObject cancelButton;
    private Vector3 tabTargetPosition;
    private Vector3 buttonTargetPosition;
    private Vector3 tabCurrentVelocity = Vector3.zero;
    private Vector3 buttonCurrentVelocity = Vector3.zero;
    protected Vector3 ofset = new Vector3(0, 5.5f, 0);
    protected float moveTime = 0.35f;

    protected virtual void Start()
    {
        cancelButton = GameObject.Find("CancelButton");
        tabTargetPosition = this.transform.position - ofset;
        buttonTargetPosition = cancelButton.transform.position - ofset;
        spriteRenderer = GetComponent<SpriteRenderer>();
        camera = Camera.main;
    }

    protected virtual void Update()
    {
        if (TabManager.Instance.getIsMove() == true && TabManager.Instance.getCurrentTopTab() == gameObject) 
        {
            //refは引用先の変更がスコープ外でも維持される変数のこと
            this.gameObject.transform.position = Vector3.SmoothDamp(this.transform.position, tabTargetPosition, ref tabCurrentVelocity, moveTime);
            cancelButton.transform.position = Vector3.SmoothDamp(cancelButton.transform.position, buttonTargetPosition, ref buttonCurrentVelocity, moveTime);
            if (Vector3.Distance(this.gameObject.transform.position, tabTargetPosition) < 0.5f) 
            {
                Debug.Log("終了");
                TabManager.Instance.setIsMove(false);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!(TabManager.Instance.getDontTouch())) 
        {
            TabManager.Instance.OnTabEnter(this.gameObject, spriteRenderer);
        }
    }

    private void OnMouseExit()
    {
        if (!(TabManager.Instance.getDontTouch())) 
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitObj = Physics2D.OverlapPoint(mousePos);
            TabManager.Instance.OnTabExit(gameObject, hitObj?.gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (!(TabManager.Instance.getDontTouch())) 
        {
            TabManager.Instance.setDontTouch(true);
            TabManager.Instance.setIsMove(true);
        };
    }
}