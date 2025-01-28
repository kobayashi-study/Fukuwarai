using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static TabController;

public class CancelButtonController : MonoBehaviour
{
    private Vector3 buttonTargetPosition;
    private Vector3 tabTargetPosition;
    private Vector3 tabCurrentVelocity = Vector3.zero;
    private Vector3 buttonCurrentVelocity = Vector3.zero;
    private Vector3 ofset = new Vector3(0, 5.5f, 0);
    private GameObject currentTopTab;
    List<MovePartManager.MovePartInfo> movePartInfos = new List<MovePartManager.MovePartInfo>();
    private float moveTime = 0.35f;
    private bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonTargetPosition = this.transform.position;
        Debug.Log("�L�����Z���{�^����Start()���Ă΂�܂���");
        buttonTargetPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove == true && currentTopTab != null)
        {
            for (int i = 0; i < movePartInfos.Count; i++) 
            {
                if (movePartInfos[i].part.GetComponent<PartsController>().InTab == true)
                {
                    Vector3 velocity = movePartInfos[i].velocity;
                    movePartInfos[i].part.transform.position = Vector3.SmoothDamp(movePartInfos[i].part.transform.position,
                                                                                  movePartInfos[i].targetPos + ofset,
                                                                                  ref velocity,
                                                                                  moveTime);
                    movePartInfos[i].velocity = velocity;
                }
            }
            transform.position = Vector3.SmoothDamp(transform.position, buttonTargetPosition, ref buttonCurrentVelocity, moveTime);
            currentTopTab.transform.position = Vector3.SmoothDamp(currentTopTab.transform.position, tabTargetPosition, ref tabCurrentVelocity, moveTime);
            if (Vector3.Distance(this.gameObject.transform.position, buttonTargetPosition) < 0.5f)
            {
                isMove = false;
                TabManager.Instance.setDontTouch(false);
            }
        }
    }
    void OnEnable()
    {
        Debug.Log("�L�����Z���{�^�����L��������܂���");
    }

    private void OnMouseEnter()
    {
        Debug.Log("�}�E�X���L�����Z���{�^���ɐG��܂���");
    }

    private void OnMouseExit()
    {
        Debug.Log("�}�E�X���L�����Z���{�^�����痣��܂���");
    }
    private void OnMouseDown()
    {
        Debug.Log("�}�E�X���N���b�N����܂���");
        currentTopTab = TabManager.Instance.getCurrentTopTab();
        movePartInfos = currentTopTab.GetComponent<TabController>().GetMovePartInfos();
        tabTargetPosition = currentTopTab.transform.position + ofset;
        isMove = true;
    }
}
