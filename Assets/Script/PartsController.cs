using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PartsController : MonoBehaviour
{
    [SerializeField] private Vector2 collision;
    private AudioSource[] audioSources;
    private Vector3 distance;
    private Vector3 validPosition;
    private Vector3 targetPosition;
    private Vector3 mouseScr;
    private Vector3 mouseWor;
    private bool inTab = true;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        validPosition = this.transform.position;
    }

    private void OnMouseDown()
    {
        audioSources[0].Play();
        Debug.Log("inTabÇÃîªíËÅF" + inTab);
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
        //Collider2D[] overlaps = Physics2D.OverlapBoxAll(targetPosition, collision, transform.rotation.eulerAngles.z);
        //ìñÇΩÇËîªíËÇéÊìæÇµÇƒÅAinTabÇtrueÇ‚falseÇ…ïœä∑Ç∑ÇÈèàóù
        Collider2D[] overlaps = Physics2D.OverlapPointAll(mouseWor);
        foreach (Collider2D overlap in overlaps)
        {
            if (!(overlap.tag == "Tag"))
            {
                inTab = false;
            }
            else 
            {
                inTab = true;
            }
        }
        Debug.Log("inTabÇÃîªíËÅF" + inTab);
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