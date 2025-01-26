using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EyeTabController : TabController
{
    [SerializeField] private GameObject[] eyes;
    public List<GameObject> objList = new List<GameObject>();
    public List<Vector3> velocityList = new List<Vector3>();
    public List<Vector3> targetPosList = new List<Vector3>();
    private Vector3 currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        objList.AddRange(eyes);
        for (int i = 0; i < objList.Count; i++)
        {
            velocityList.Add(currentVelocity);
            Vector3 targetPosition = objList[i].transform.position - ofset;
            targetPosList.Add(targetPosition);
        }
        Debug.Log(objList[0].transform.position - ofset);
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (TabManager.Instance.getIsMove() == true && TabManager.Instance.getCurrentTopTab() == gameObject)
        {
            for (int i = 0; i < objList.Count; i++)
            {
                Vector3 velocity = velocityList[i];
                objList[i].transform.position = Vector3.SmoothDamp(objList[i].transform.position,
                                                                    targetPosList[i],
                                                                    ref velocity, 
                                                                    moveTime);
                velocityList[i] = velocity;
            }
        }
    }
}
