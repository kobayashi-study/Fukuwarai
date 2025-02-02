using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreController : MonoBehaviour
{
    //�����������Č����ڂ������I�u�W�F�N�g�iSYMMETRY�j�Ƃ����łȂ��I�u�W�F�N�g(NORMAL)�ō̓_�������قȂ�̂ŁA�t���O�Ƃ��Ē萔�uCATEGORY�v���쐬�B
    private enum CATEGORY 
    {
        NORMAL,
        SYMMETRY,
    }

    //���O�̂��߁A�Ǘ��p�t���O�Ƃ��Ē萔�uTYPE�v������Ă����B
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
        //��SYMMETRY�̂ݔ���p�ɕϐ���������K�v�Ȃ̂ŁAVector3?�^(�L�q���Ȃ��ꍇ��null������^)�̕ϐ��usecondlyPosition�v���쐬�BSYMMETRY�̏ꍇ�����g�p����B
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
    //�������̍��W���
    private Vector3 rightEyebrowA = new Vector3(-5.68f, 0.954f, -1);
    private Vector3 leftEyebrowA = new Vector3(-4.27f, 0.954f, -1);
    private Vector3 rightEyeA = new Vector3(-6.33f, -0.236f, -1);
    private Vector3 leftEyeA = new Vector3(-3.617f, -0.236f, -1);
    private Vector3 noseA = new Vector3(-4.96f, -1.06f, -1);
    private Vector3 rightCheekA = new Vector3(-6.95f, -1.243f, -1);
    private Vector3 leftCheekA = new Vector3(-2.997f, -1.243f, -1);
    private Vector3 mouthA = new Vector3(-4.978f, -1.872f, -1);
    //��CalculateScore�̌J��Ԃ������Ɏg�p����List�upartInfos�v���쐬�B
    private List<PartInfo> partInfos = new List<PartInfo>();
    //���̓_���Ɏg�p����_��
    private float score = 0;
    private float perfectDistance = 0.5f;
    private float goodDistance = 1;
    private float perfectScore = 10;
    private float goodScore = 5;
    //���uSymmetricPartsCal�v���\�b�h�̃t���O�Ǘ��p�B
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
    //���̓_�p���\�b�h
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
    //���̓_�p���\�b�h�iSYMMETRY�̏ꍇ�j
    private float SymmetricPartsCal(PartInfo facePart) 
    {
        //��MaxValue�����ď��������邱�ƂŁA��x�ڂ̔���̍ۂɊ��p�ł���B
        float rightDis = float.MaxValue;
        float leftDis = float.MaxValue;
        float distance = 0;

        Debug.Log(facePart.gameObject.transform.localPosition);
        //�����\�b�h���Ċ��p�ł���悤�Ƀt���O�����Z�b�g�B
        if (rightUsed == true && leftUsed == true)
        {
            rightUsed = false;
            leftUsed = false;
        }
        //���urightUsed��true(=���ɔ��肪�̗p����Ă���ꍇ)�v�͏������s�Ȃ�Ȃ��悤�ɂ��邱�Ƃŏd����h��
        if (rightUsed == false)
        {
            rightDis = Vector3.Distance(facePart.gameObject.transform.localPosition, facePart.primaryPosition);
            Debug.Log("�I�u�W�F�N�g�Ƃ̋���(rightDistance):" + rightDis);
        }
        if (leftUsed == false)
        {
            leftDis = Vector3.Distance(facePart.gameObject.transform.localPosition, facePart.secondlyPosition.Value);
            Debug.Log("�I�u�W�F�N�g�Ƃ̋���(leftDistance):" + leftDis);
        }

        if (rightDis < leftDis)
        {
            distance = rightDis;
            rightUsed = true;
            Debug.Log("rightDistance���̗p�F" + distance);
        }
        else 
        {
            distance = leftDis;
            leftUsed = true;
            Debug.Log("lefhtDistance���̗p�F" + distance);
        }
        addScore(distance);
        return score;
    }
    //�����_�p���\�b�h
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
