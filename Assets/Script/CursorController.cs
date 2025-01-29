using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class CursorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Texture2D touch;
    private new Camera camera;
    void Start()
    {
        camera = Camera.main;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        //�J�[�\���̍��W�����[���h���W�ɕϊ�
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        //�ϊ������J�[�\���̍��W�̉��ɂ�����̂�OverlapPoint�Ŏ擾
        Collider2D[] hitObj = Physics2D.OverlapPointAll(mousePos);

        //OverlapPoint�Ŏ擾����Collider2D(=GameObject�̃R���|�[�l���g)��null�ł͂Ȃ�+�^�O�t�������ɐݒ�
        if (hitObj != null) 
        {
            for (int i = 0; i < hitObj.Count(); i++)
            {
                //Debug.Log("���o���ꂽ�I�u�W�F�N�g: " + hitObj[i].gameObject.name + "�^�O: " + hitObj[i].tag);
                if (hitObj[i].tag == "FaceParts" || hitObj[i].tag == "Button")
                {
                    Debug.Log("�t�F�C�X�p�[�c���{�^���ɐG��Ă��܂�");
                    Cursor.SetCursor(touch, Vector2.zero, CursorMode.Auto);
                    break;
                }
                else if (hitObj[i].tag == "Tab")
                {

                    Debug.Log("�^�u�ɐG��Ă��܂�");
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
            }
        }
        else 
        {
            //�G��Ă��Ȃ��ꍇ�͌��̃J�[�\���摜�ɖ߂�
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        /*
        if (hitObj != null)
        {
            Debug.Log($"���o���ꂽ�I�u�W�F�N�g: {hitObj.gameObject.name}, �^�O: {hitObj.tag}");
        }
        else
        {
            Debug.Log("Collider�����o����܂���ł���");
        }
        */
    }
}
