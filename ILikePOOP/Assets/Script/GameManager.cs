using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI Text_Score;
    public Image HpBar_Fill;

    public GameObject[] DropObject;

    float time;

    public int score;
    public float Player_Hp;

    void Start()
    {
        instance = this;
        score = 0;
        Player_Hp = 100;
        time = 0;

        Text_Score.text = $"{score}";
    }

    void Update()
    {
        time += Time.deltaTime;
        Player_Hp -= Time.deltaTime * 2f;

        if (time >= 1f)
        {
            GameObject dropObject = DropObject[Random.Range(0, 2)];
            dropObject.transform.position = new Vector2(
                Random.Range(-2.5f, 2.5f), 6);
            Instantiate(dropObject);

            time = 0;
        }

        HpBar_Fill.fillAmount = Player_Hp / 100;
        Text_Score.text = $"{score}";
    }
}
