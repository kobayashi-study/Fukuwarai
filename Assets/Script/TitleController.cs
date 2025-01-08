using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private float minimum = 1.3f;
    private float magSpeed = 3.0f;
    private float magnification = 0.05f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector2(this.minimum + Mathf.Sin(Time.time * this.magSpeed) * this.magnification,
                                                this.minimum + Mathf.Sin(Time.time * this.magSpeed) * this.magnification);

    }
}
