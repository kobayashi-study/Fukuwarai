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
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 ofset = new Vector3(0, 5.5f, 0);
    private float moveTime = 0.35f;

    void Start()
    {
        cancelButton = GameObject.Find("CancelButton");
        tabTargetPosition = this.transform.position - ofset;
        buttonTargetPosition = cancelButton.transform.position - ofset;
        spriteRenderer = GetComponent<SpriteRenderer>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (TabManager.Instance.getIsMove() == true && TabManager.Instance.getCurrentTopTab() == gameObject) 
        {
            this.gameObject.transform.position = Vector3.SmoothDamp(this.transform.position, tabTargetPosition, ref currentVelocity, moveTime);
            cancelButton.transform.position = Vector3.SmoothDamp(cancelButton.transform.position, buttonTargetPosition, ref currentVelocity, moveTime);
            if (Vector3.Distance(this.gameObject.transform.position, tabTargetPosition) < 0.5f) 
            {
                Debug.Log("I—¹");
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