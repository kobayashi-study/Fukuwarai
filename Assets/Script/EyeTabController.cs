using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTabController : TabController
{
    [SerializeField] private GameObject[] eyes;
    private List<GameObject> eyeList = new List<GameObject>();
    private List<Vector3> velocityList = new List<Vector3>();
    private Vector3 currentVelocity = Vector3.zero;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        eyeList.AddRange(eyes);
        for (int i = 0; i < eyeList.Count; i++)
        {
            velocityList.Add(currentVelocity);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (TabManager.Instance.getIsMove() == true && TabManager.Instance.getCurrentTopTab() == gameObject)
        {
            for (int i = 0; i < eyeList.Count; i++)
            {
                Vector3 velocity = velocityList[i];
                Vector3 targetPosition = eyeList[i].transform.position - ofset;
                eyeList[i].transform.position = Vector3.SmoothDamp(eyeList[i].transform.position, 
                                                                    targetPosition,
                                                                    ref velocity, 
                                                                    moveTime);
                velocityList[i] = velocity;
                /*
                if (Vector3.Distance(eyesList[i].transform.position, eyesList[i].transform.position - ofset) < 0.5f)
                {
                    TabManager.Instance.setDontTouch(false);
                }
                */
            }
        }
    }
}
