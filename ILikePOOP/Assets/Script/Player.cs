using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DropObject;

public class Player : MonoBehaviour
{
    GameManager gm;

    SpriteRenderer sr;
    Animator animator;

    Vector2 PlayerMoveVec;

    float speed = 5f;

    private void Start()
    {
        gm = GameManager.instance;
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        PlayerMoveVec = Vector2.zero;
        transform.position = new Vector3(0, -3f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            PlayerMoveVec = Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow))
            PlayerMoveVec = Vector2.right;

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("isRun", true);

            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3
                (Input.mousePosition.x, Input.mousePosition.y, 0));

            if (point.x <= 0.5) { PlayerMoveVec = Vector2.left; sr.flipX = false; }
            else { PlayerMoveVec = Vector2.right; sr.flipX = true; }
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isRun", false);
        }

        Moving();
    }

    // 플레이어 이동
    void Moving()
    {
        transform.Translate(PlayerMoveVec * speed * Time.deltaTime);

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -2.5f, 2.5f), transform.position.y);
    }

    // 오브젝트 충돌 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor")) return; //바닥 충돌 제외

        if (collision.CompareTag("Object"))
        {
            DropObject dropObject = collision.GetComponent<DropObject>();

            gm.score += dropObject.ObjScore;
            gm.Player_Hp += dropObject.ObjHp;
            if (gm.score < 0) gm.score = 0;
            if (gm.Player_Hp > 100) gm.Player_Hp = 100;


            dropObject.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Object_B")
        {
            DropObject dropObject = collision.gameObject.GetComponent<DropObject>();

            gm.score += dropObject.ObjScore;
            gm.Player_Hp += dropObject.ObjHp;
            if (gm.Player_Hp > 100) gm.Player_Hp = 100;

            gm.BonusObjCount--;
            dropObject.gameObject.SetActive(false);
        }
    }
}