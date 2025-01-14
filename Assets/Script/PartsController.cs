using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PartsController : MonoBehaviour
{
    private AudioSource[] audioSources;
    private bool isMove = true;
    private Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        audioSources = GetComponents<AudioSource>();
        audioSources[0].Play();
        Vector3 mouseScr = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 mouseWor = Camera.main.ScreenToWorldPoint(mouseScr);
        distance = this.transform.position - mouseWor;
    }

    //↓顔のパーツをドラッグ操作できる
    private void OnMouseDrag()
    {
        Vector3 mouseScr = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 mouseWor = Camera.main.ScreenToWorldPoint(mouseScr);

        if (isMove)
        {
            this.transform.position = mouseWor + distance;
        }
    }

    private void OnMouseUp()
    {
        audioSources = GetComponents<AudioSource>();
        audioSources[1].Play();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Button")
        {
            isMove = false;
        }
    }*/
}
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Button")
        {
            Debug.Log("接触終了");
            isMove = true;
        }
    }
    
}

/*
public class PartsController : MonoBehaviour
{
    private AudioSource[] audioSources;
    private GameObject button;
    private bool isMove = true;

    void Start()
    {
        button = GameObject.FindGameObjectWithTag("Button");
    }

    void OnMouseDown()
    {
        audioSources = GetComponents<AudioSource>();
        audioSources[0].Play();
    }

    void OnMouseDrag()
    {
        Vector3 mouseScr = Input.mousePosition;
        Vector3 mouseWor = Camera.main.ScreenToWorldPoint(new Vector3(mouseScr.x, mouseScr.y, 1));


        float distance = Vector2.Distance(mouseWor, button.transform.position);
        if (distance < 1)
        {
            isMove = false;
        }

        if (isMove)
        {
            transform.position = mouseWor;
        }
    }

    void OnMouseUp()
    {
        audioSources = GetComponents<AudioSource>();
        audioSources[1].Play();
    }
}
*/
