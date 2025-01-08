using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Sprite click;
    [SerializeField] string sceneName;
    [SerializeField] float waitSeconds;
    private AudioSource audioSource;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        this.transform.localScale = new Vector2(2.1f, 2.0f);
    }

    private void OnMouseExit()
    {
        this.transform.localScale = new Vector2(1.6f, 1.5f);
    }
    private void OnMouseDown()
    {
        StartCoroutine(SceneChange());
    }

    protected virtual void SpriteChange() 
    {
        this.GetComponent<SpriteRenderer>().sprite = click;
    }
    protected virtual IEnumerator SceneChange()
    {
        SpriteChange();
        audioSource.Play();
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(sceneName);
    }
}
