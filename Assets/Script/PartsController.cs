using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PartsController : MonoBehaviour
{   
    private AudioSource[] audioSources;
    private bool isMove = true;
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
    }


    //↓顔のパーツをドラッグ操作できる
    private void OnMouseDrag()
    {
        Vector3 mouse3D =  new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        Vector3 mouse2D = Camera.main.ScreenToWorldPoint(mouse3D);

        if(isMove) 
        {
            this.transform.position = mouse2D;
        }
    }

    private void OnMouseUp()
    {
        audioSources = GetComponents<AudioSource>();
        audioSources[1].Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Button") 
        {
            Debug.Log("接触中");
            isMove = false;
        }
    }
}
