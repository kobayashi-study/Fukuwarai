using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    AudioSource audioSource;
    private GameObject scoreText;
    private float score;
    private float highScore = 60;
    private float middleScore = 40;
    private float lowScore = 20;
    private float waitSeconds = 1;
    private Vector3 primaryPosition = new Vector3(-7, 0, 0);
    private Vector3 secondlyPosition = new Vector3(0, 0, 0);
    private Vector3 thirdPosition = new Vector3(7, 0, 0);
    List<Vector3> positions = new List<Vector3>();
    public GameObject starPrefab;
    public GameObject starEmptyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreText = GameObject.Find("ScoreText2");
        score = ScoreManager.GetScore();
        Debug.Log(score);
        positions.Add(primaryPosition);
        positions.Add(secondlyPosition);
        positions.Add(thirdPosition);
        StartCoroutine(StarGenerate());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator StarGenerate()
    {
        if (score >= highScore)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                yield return new WaitForSeconds(waitSeconds);
                GameObject star = Instantiate(starPrefab);
                star.transform.position = positions[i];
            }
            this.scoreText.GetComponent<Text>().text = "êØÇRÇ¬ÅIÅIÅI";
        }
        else if (score >= middleScore)
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                yield return new WaitForSeconds(waitSeconds);
                GameObject star = Instantiate(starPrefab);
                star.transform.position = positions[i];
            }
            yield return new WaitForSeconds(waitSeconds);
            GameObject emptyStar = Instantiate(starEmptyPrefab);
            emptyStar.transform.position = thirdPosition;
            this.scoreText.GetComponent<Text>().text = "êØÇQÇ¬ÅIÅI";
        }
        else if (score >= lowScore)
        {
            yield return new WaitForSeconds(waitSeconds);
            GameObject star = Instantiate(starPrefab);
            star.transform.position = primaryPosition;
            for (int i = 1; i < positions.Count; i++)
            {
                yield return new WaitForSeconds(waitSeconds);
                GameObject emptyStar = Instantiate(starEmptyPrefab);
                emptyStar.transform.position = positions[i];
            }
            this.scoreText.GetComponent<Text>().text = "êØÇPÇ¬ÅI";
        }
        else if (score < lowScore)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                yield return new WaitForSeconds(waitSeconds);
                GameObject emptyStar = Instantiate(starEmptyPrefab);
                emptyStar.transform.position = positions[i];
            }
            this.scoreText.GetComponent<Text>().text = "êØÇÕÇOÇ±Åc";
        }
        yield return new WaitForSeconds(1.5f);
        scoreText.GetComponent<Text>().enabled = true;
        audioSource.Play();
    }
}
