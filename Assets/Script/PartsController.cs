using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PartsController : MonoBehaviour
{
    [SerializeField] private Vector2 buttonCollision;
    [SerializeField] private Vector2 indexCollision;
    private AudioSource[] audioSources;
    private SpriteRenderer spriteRenderer;
    private GameObject currentTopTab;
    private Vector3 distance;
    private Vector3 validPosition;
    private Vector3 targetPosition;
    private Vector3 mouseScr;
    private Vector3 mouseWor;
    private bool inTab = true;
    public bool InTab 
    { 
        get { return inTab; } 
        set { inTab = value; } 
    }

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        validPosition = this.transform.position;
    }

    private void OnMouseDown()
    {
        audioSources[0].Play();
        currentTopTab = TabManager.Instance.getCurrentTopTab();
        spriteRenderer.sortingOrder = 9;
        mouseScr = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(this.transform.position).z);
        mouseWor = Camera.main.ScreenToWorldPoint(mouseScr);
        distance = this.transform.position - mouseWor;
    }

    private void OnMouseDrag()
    {
        mouseScr = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(this.transform.position).z);
        mouseWor = Camera.main.ScreenToWorldPoint(mouseScr);
        targetPosition = mouseWor + distance;

        MoveObject();
    }

    private void OnMouseUp()
    {
        audioSources[1].Play();
        //ìñÇΩÇËîªíËÇéÊìæÇµÇƒÅAinTabÇtrueÇ‚falseÇ…ïœä∑Ç∑ÇÈèàóù
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(mouseWor, 0.5f);
        Collider2D[] tabColliders = currentTopTab.GetComponents<BoxCollider2D>();

        foreach (Collider2D overlap in overlaps)
        {
            if (overlap == tabColliders[0])
            {
                inTab = true;
                spriteRenderer.sortingOrder = 9;
                break;
            }
            else 
            {
                inTab = false;
                spriteRenderer.sortingOrder = 7;
            }
        }
    }

    private void MoveObject()
    {
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(targetPosition, buttonCollision, transform.rotation.eulerAngles.z);
        Collider2D[] tabCheck = Physics2D.OverlapBoxAll(targetPosition, indexCollision, transform.rotation.eulerAngles.z);
        Collider2D[] tabColliders = currentTopTab.GetComponents<BoxCollider2D>();

        bool dontMove = false;

        foreach (Collider2D overlap in overlaps)
        {
            Debug.Log(overlap);
            if (overlap.tag == "Button" || overlap.tag == "Fence")
            {
                dontMove = true;
                break;
            }
        }
        for (int i = 0; i < tabCheck.Length; i++) 
        {
            if (tabCheck[i] == tabColliders[1])
            {
                dontMove = true;
                break;
            }
        }
        if (!dontMove)
        {
            transform.position = targetPosition;
            validPosition = transform.position;
        }
        else
        {
            transform.position = validPosition;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("è’ìÀåüèo: " + collision.gameObject.name);
    }
}