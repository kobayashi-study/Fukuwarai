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
        //カーソルの座標をワールド座標に変換
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        //変換したカーソルの座標の下にあるものをOverlapPointで取得
        Collider2D hitObj = Physics2D.OverlapPoint(mousePos);

        //OverlapPointで取得したCollider2D(=GameObjectのコンポーネント)がnullではない+タグ付を条件に設定
        if (hitObj != null)
        {
            if (hitObj.tag == "FaceParts" || hitObj.tag == "Button")
            {
                Cursor.SetCursor(touch, Vector2.zero, CursorMode.Auto);
            }
        }
        else 
        {
            //触れていない場合は元のカーソル画像に戻す
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
