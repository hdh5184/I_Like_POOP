using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DropObject;

public class Player : MonoBehaviour
{
    GameManager gm;

    float speed = 5f;

    void Awake()
    {
        gm = GameManager.instance;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))    Moving(Vector2.left);
        if (Input.GetKey(KeyCode.RightArrow))   Moving(Vector2.right);

        if (Input.GetMouseButton(0))
        {
            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3
                (Input.mousePosition.x, Input.mousePosition.y, 0));

            if (point.x <= 0.5) Moving(Vector2.left);
            else Moving(Vector2.right);
        }
    }

    void Moving(Vector2 MovingVec)
    {
        transform.Translate(MovingVec * speed * Time.deltaTime);

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -2.5f, 2.5f), transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor")) return; //바닥 충돌 제외

        //////////////임시 구현//////////////
        if (collision.CompareTag("Object_A"))
        {
            gm.score += 10;
            gm.Player_Hp += 5;
            if (gm.Player_Hp > 100) gm.Player_Hp = 100;
        }
        if (collision.CompareTag("Object_B"))
        {
            gm.score -= 20;
            gm.Player_Hp -= 10;
        }
        ////////////////////////////////////

        DropObject dropObject = collision.GetComponent<DropObject>();

        /* 
         * 플레이어 체력은 100을 넘어가지 않도록 조정 필요
         */


        dropObject.gameObject.SetActive(false);
    }
}
