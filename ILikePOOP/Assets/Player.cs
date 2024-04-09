using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gm;

    float speed;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        speed = 3f;
    }

    // Update is called once per frame
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object_A"))
        {
            gm.score += 10; gm.Player_Hp += 5;
        }
        if (collision.CompareTag("Object_B"))
        {
            gm.score -= 20; gm.Player_Hp -= 10;
        }

        Destroy(collision.gameObject);

        Debug.Log(gm.score);
    }
}
