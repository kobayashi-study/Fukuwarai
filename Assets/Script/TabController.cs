using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class TabController : MonoBehaviour
{
    [SerializeField] private GameObject[] parts;
    private new Camera camera;
    private SpriteRenderer spriteRenderer;
    private GameObject cancelButton;
    private List<MovePartManager.MovePartInfo> movePartInfos = new List<MovePartManager.MovePartInfo>();
    private List<Vector3> clickPos = new List<Vector3>();
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
        for (int i = 0; i < parts.Count(); i++)
        {
            MovePartManager.MovePartInfo movePartInfo = new MovePartManager.MovePartInfo(parts[i], Vector3.zero, parts[i].transform.position - ofset);
            movePartInfos.Add(movePartInfo);
        }

    }

    protected virtual void Update()
    {
        if (TabManager.Instance.getIsMove() == true && TabManager.Instance.getCurrentTopTab() == gameObject) 
        {
            for (int i = 0; i < movePartInfos.Count; i++)
            {
                if (movePartInfos[i].part.GetComponent<PartsController>().InTab == true) 
                {
                    Vector3 velocity = movePartInfos[i].velocity;
                    movePartInfos[i].part.transform.position = Vector3.SmoothDamp(movePartInfos[i].part.transform.position,
                                                                                  clickPos[i] - ofset,
                                                                                  ref velocity,
                                                                                  moveTime);
                    movePartInfos[i].velocity = velocity;
                }
            }
            //refは引用先の変更がスコープ外でも維持される変数のこと
            this.gameObject.transform.position = Vector3.SmoothDamp(this.transform.position, tabTargetPosition, ref tabCurrentVelocity, moveTime);
            cancelButton.transform.position = Vector3.SmoothDamp(cancelButton.transform.position, buttonTargetPosition, ref buttonCurrentVelocity, moveTime);
            if (Vector3.Distance(this.gameObject.transform.position, tabTargetPosition) < 0.5f) 
            {
                Debug.Log("終了");
                TabManager.Instance.setIsMove(false);
            }
        }
        if (TabManager.Instance.getIsMove() == true && TabManager.Instance.getCurrentTopTab() == gameObject)
        {
            //最前面のタブがこのオブジェクトなら何もしない
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
            clickPos.Clear();
            for (int i = 0; i < movePartInfos.Count; i++)
            {
                clickPos.Add(movePartInfos[i].part.transform.position);
            }
            TabManager.Instance.setDontTouch(true);
            TabManager.Instance.setIsMove(true);
        };
    }

    public List<MovePartManager.MovePartInfo> GetMovePartInfos ()
    {
        return this.movePartInfos;
    }
}