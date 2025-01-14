using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Linq;

public class StarGeneretor : MonoBehaviour
{
    [SerializeField] BGMController bgmController;
    AudioSource[] audioSources;
    private GameObject scoreText;
    private float currentScore;
    private float highScore = 60;
    private float midScore = 40;
    private float lowScore = 20;
    private float waitSeconds = 1;
    Vector3[] positions = new[]
    {
        new Vector3(-7, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(7, 0, 0)
    };

    private Dictionary<int, string> scoreInfo = new Dictionary<int, string>();
    public GameObject starPrefab;
    public GameObject starEmptyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        scoreText = GameObject.Find("ScoreText2");
        currentScore = GameManager.GetScore();
        Debug.Log(currentScore);

        Dictionary<int, string> info = scoreRange(currentScore);
        StartCoroutine(StarGenerate(currentScore, info));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Dictionary<int, string> scoreRange(float score) 
    {
        if (score >= highScore) return new Dictionary<int, string> { { 3, "êØÇRÇ¬ÅIÅIÅI" } } ;
        if (score >= midScore) return new Dictionary<int, string> { { 2, "êØÇQÇ¬ÅIÅI" } };
        if (score >= lowScore) return new Dictionary<int, string> { { 1, "êØÇPÇ¬ÅI" } };
        return new Dictionary<int, string> { { 0, "êØÇÕÇOÇ±Åc" } };
    }

    IEnumerator StarGenerate(float score, Dictionary<int, string> info)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            yield return new WaitForSeconds(waitSeconds);
            if (i < info.Keys.First())
            {
                GameObject star = Instantiate(starPrefab);
                star.transform.position = positions[i];
            }
            else 
            {
                GameObject starEmpty = Instantiate(starEmptyPrefab);
                starEmpty.transform.position = positions[i];
            }
        }
        this.scoreText.GetComponent<Text>().text = info.Values.First();
        yield return new WaitForSeconds(1.5f);
        scoreText.GetComponent<Text>().enabled = true;
        PlayAudio();
        yield return new WaitForSeconds(1.5f);
        bgmController.GetComponent<BGMController>().enabled = true;
    }

    private void PlayAudio() 
    {
        if (currentScore > lowScore)
        {
            audioSources[0].Play();
        }
        else 
        {
            audioSources[1].Play();
        }
    }
}
