using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    private float rotate = 45f;
    private float Speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    } 

    void Update()
    {
        float angle = rotate * Mathf.Sin(Time.time * Speed);
        switch (this.gameObject.name) 
        {
            case "Oni":
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
                break;

            case "Okame":
                transform.rotation = Quaternion.Euler(0f, 0f, -angle);
                break;
        }
    }
}
