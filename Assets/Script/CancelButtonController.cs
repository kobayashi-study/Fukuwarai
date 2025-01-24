using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButtonController : MonoBehaviour
{
    private Vector3 buttonTargetPosition;
    private Vector3 tabTargetPosition;
    private Vector3 tabCurrentVelocity = Vector3.zero;
    private Vector3 buttonCurrentVelocity = Vector3.zero;
    private Vector3 ofset = new Vector3(0, 5.5f, 0);
    private GameObject currentTopTab;
    private float moveTime = 0.35f;
    private bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonTargetPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove == true && currentTopTab != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, buttonTargetPosition, ref buttonCurrentVelocity, moveTime);
            currentTopTab.transform.position = Vector3.SmoothDamp(currentTopTab.transform.position, tabTargetPosition, ref tabCurrentVelocity, moveTime);
            if (Vector3.Distance(this.gameObject.transform.position, buttonTargetPosition) < 0.5f)
            {
                isMove = false;
                TabManager.Instance.setDontTouch(false);
            }
        }
    }

    private void OnMouseDown()
    {
        currentTopTab = TabManager.Instance.getCurrentTopTab();
        tabTargetPosition = currentTopTab.transform.position + ofset;
        isMove = true;
    }
}
