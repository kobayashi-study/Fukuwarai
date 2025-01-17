using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PartsController : MonoBehaviour
{
    [SerializeField] private Vector2 collision;
    private AudioSource[] audioSources;
    private Vector3 distance;
    private Vector3 validPosition;
    private Vector3 targetPosition;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        validPosition = this.transform.position;
    }

    private void OnMouseDown()
    {
        audioSources[0].Play();
        Vector3 mouseScr = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(this.transform.position).z);
        Vector3 mouseWor = Camera.main.ScreenToWorldPoint(mouseScr);
        distance = this.transform.position - mouseWor;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseScr = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(this.transform.position).z);
        Vector3 mouseWor = Camera.main.ScreenToWorldPoint(mouseScr);
        targetPosition = mouseWor + distance;

        MoveObject();
    }

    private void OnMouseUp()
    {
        audioSources[1].Play();
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