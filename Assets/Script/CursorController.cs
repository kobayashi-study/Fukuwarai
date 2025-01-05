using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CursorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Texture2D touch;
    private Camera camera;
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
        Collider2D hitObj = Physics2D.OverlapPoint(mousePos);

        //OverlapPoint�Ŏ擾����Collider2D(=GameObject�̃R���|�[�l���g)��null�ł͂Ȃ�+�^�O�t�������ɐݒ�
        if (hitObj != null)
        {
            if (hitObj.tag == "FaceParts" || hitObj.tag == "Button")
            {
                Cursor.SetCursor(touch, Vector2.zero, CursorMode.Auto);
            }
        }
        else 
        {
            //�G��Ă��Ȃ��ꍇ�͌��̃J�[�\���摜�ɖ߂�
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
