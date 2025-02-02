using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PartsController : MonoBehaviour
{
    [SerializeField] private Vector2 collision;
    private AudioSource[] audioSources;
    private SpriteRenderer spriteRenderer;
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
        Collider2D[] overlaps = Physics2D.OverlapPointAll(mouseWor);
        foreach (Collider2D overlap in overlaps)
        {
            if (!(overlap.tag == "Tab"))
            {
                inTab = false;
                spriteRenderer.sortingOrder = 7;
            }
            else 
            {
                inTab = true;
                spriteRenderer.sortingOrder = 9;
                break;
            }
        }
    }

    private void MoveObject()
    {
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(targetPosition, collision, transform.rotation.eulerAngles.z);
        bool dontMove = false;

        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.tag == "Button")
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
}