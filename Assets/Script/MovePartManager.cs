using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public class MovePartInfo
    {
        public GameObject part;
        public Vector3 velocity;
        public Vector3 targetPos;
        public MovePartInfo(GameObject part, Vector3 velocity, Vector3 targetPos)
        {
            this.part = part;
            this.velocity = velocity;
            this.targetPos = targetPos;
        }
    }
}
