using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    BoxCollider2D boxCollider;
    //Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TabManager.Instance.getDontTouch()) 
        {
            boxCollider.enabled = true;
        }
        else 
        {
            boxCollider.enabled = false;
        }
    }


}
