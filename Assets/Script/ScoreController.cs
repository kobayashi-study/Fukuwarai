using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreController : MonoBehaviour
{
    //↓複数あって見た目が同じオブジェクト（SYMMETRY）とそうでないオブジェクト(NORMAL)で採点処理が異なるので、フラグとして定数「CATEGORY」を作成。
    private enum CATEGORY 
    {
        NORMAL,
        SYMMETRY,
    }

    //↓念のため、管理用フラグとして定数「TYPE」を作っておく。
    private enum TYPE
    { 
        RIGHTEYEBROW,
        LEFTEYEBROW,
        RIGHTEYE,
        LEFTEYE,
        NOSE,
        RIGHTCHEEK,
        LEFTCHEEK,
        MOUTH,
    }

    struct PartInfo 
    { 
        public GameObject gameObject;
        public CATEGORY category;
        public TYPE type;
        public Vector3 primaryPosition;
        //↓SYMMETRYのみ判定用に変数がもう一つ必要なので、Vector3?型(記述がない場合はnullが入る型)の変数「secondlyPosition」を作成。SYMMETRYの場合だけ使用する。
        public Vector3? secondlyPosition;
        public PartInfo(GameObject gameObject, CATEGORY category, TYPE type, Vector3 positionA, Vector3? positionA2 = null) 
        {
            this.gameObject = gameObject;
            this.category = category;
            this.type = type;
            this.primaryPosition = positionA;
            this.secondlyPosition = positionA2;
        }
    }
    //↓正解の座標情報
    private Vector3 rightEyebrowA = new Vector3(-5.68f, 0.954f, -1);
    private Vector3 leftEyebrowA = new Vector3(-4.27f, 0.954f, -1);
    private Vector3 rightEyeA = new Vector3(-6.33f, -0.236f, -1);
    private Vector3 leftEyeA = new Vector3(-3.617f, -0.236f, -1);
    private Vector3 noseA = new Vector3(-4.96f, -1.06f, -1);
    private Vector3 rightCheekA = new Vector3(-6.95f, -1.243f, -1);
    private Vector3 leftCheekA = new Vector3(-2.997f, -1.243f, -1);
    private Vector3 mouthA = new Vector3(-4.978f, -1.872f, -1);
    //↓CalculateScoreの繰り返し処理に使用するList「partInfos」を作成。
    private List<PartInfo> partInfos = new List<PartInfo>();
    //↓採点時に使用する点数
    private float score = 0;
    private float perfectDistance = 0.5f;
    private float goodDistance = 1;
    private float perfectScore = 10;
    private float goodScore = 5;
    //↓「SymmetricPartsCal」メソッドのフラグ管理用。
    bool rightUsed = false;
    bool leftUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        PartInfo rightEyebrow = new PartInfo(GameObject.Find("Okame_Eyebrow1"), CATEGORY.SYMMETRY, TYPE.RIGHTEYEBROW, rightEyebrowA, leftEyebrowA);
        PartInfo leftEyebrow = new PartInfo(GameObject.Find("Okame_Eyebrow2"), CATEGORY.SYMMETRY, TYPE.LEFTEYEBROW, rightEyebrowA, leftEyebrowA);
        PartInfo rightEye = new PartInfo(GameObject.Find("Okame_RightEye"), CATEGORY.NORMAL, TYPE.RIGHTEYE, rightEyeA);
        PartInfo leftEye = new PartInfo(GameObject.Find("Okame_LeftEye"), CATEGORY.NORMAL, TYPE.LEFTEYE, leftEyeA);
        PartInfo nose = new PartInfo(GameObject.Find("Okame_Nose"), CATEGORY.NORMAL, TYPE.NOSE, noseA);
        PartInfo rightCheek = new PartInfo(GameObject.Find("Okame_Cheek1"), CATEGORY.SYMMETRY, TYPE.RIGHTCHEEK, rightCheekA, leftCheekA);
        PartInfo leftCheek = new PartInfo(GameObject.Find("Okame_Cheek2"), CATEGORY.SYMMETRY, TYPE.LEFTCHEEK, rightCheekA, leftCheekA);
        PartInfo mouth = new PartInfo(GameObject.Find("Okame_Mouth"), CATEGORY.NORMAL, TYPE.MOUTH, mouthA);

        partInfos.Add(rightEyebrow);
        partInfos.Add(leftEyebrow);
        partInfos.Add(rightEye);
        partInfos.Add(leftEye);
        partInfos.Add(nose);
        partInfos.Add(rightCheek);
        partInfos.Add(leftCheek);
        partInfos.Add(mouth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //↓採点用メソッド
    public float CalculateScore() 
    {
        for(int i = 0; i <partInfos.Count; i++) 
        {
            if (partInfos[i].category == CATEGORY.NORMAL)
            {
                float distance = Vector3.Distance(partInfos[i].gameObject.transform.localPosition, partInfos[i].primaryPosition);
                Debug.Log(distance);
                addScore(distance);
            }
            else 
            {
                SymmetricPartsCal(partInfos[i]);
            }
        }
        return score;
    }
    //↓採点用メソッド（SYMMETRYの場合）
    private float SymmetricPartsCal(PartInfo facePart) 
    {
        //↓MaxValueを入れて初期化することで、二度目の判定の際に活用できる。
        float rightDis = float.MaxValue;
        float leftDis = float.MaxValue;
        float distance = 0;

        Debug.Log(facePart.gameObject.transform.localPosition);
        //↓メソッドを再活用できるようにフラグをリセット。
        if (rightUsed == true && leftUsed == true)
        {
            rightUsed = false;
            leftUsed = false;
        }
        //↓「rightUsedがtrue(=既に判定が採用されている場合)」は処理を行なわないようにすることで重複を防ぐ
        if (rightUsed == false)
        {
            rightDis = Vector3.Distance(facePart.gameObject.transform.localPosition, facePart.primaryPosition);
            Debug.Log("オブジェクトとの距離(rightDistance):" + rightDis);
        }
        if (leftUsed == false)
        {
            leftDis = Vector3.Distance(facePart.gameObject.transform.localPosition, facePart.secondlyPosition.Value);
            Debug.Log("オブジェクトとの距離(leftDistance):" + leftDis);
        }

        if (rightDis < leftDis)
        {
            distance = rightDis;
            rightUsed = true;
            Debug.Log("rightDistanceを採用：" + distance);
        }
        else 
        {
            distance = leftDis;
            leftUsed = true;
            Debug.Log("lefhtDistanceを採用：" + distance);
        }
        addScore(distance);
        return score;
    }
    //↓加点用メソッド
    private void addScore(float distance) 
    {
        if (distance <= perfectDistance)
        {
            score += perfectScore;
        }
        else if (distance <= goodDistance)
        {
            score += goodScore;
        }
    }
}
