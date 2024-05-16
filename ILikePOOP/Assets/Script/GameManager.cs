using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 관리
    public static GameManager instance;
    public PoolManager pool;

    // UI
    public TextMeshProUGUI Text_Score;
    public Image HpBar_Fill;
    public TMP_Text GmaeOverText;
    public GameObject RestartButton;

    // Background 스프라이트 저장
    public Sprite[] BackgroundSprite;
    public SpriteRenderer Background;

    public GameObject Player;
  

    // 오브젝트 생성 타겟 (랜덤)
    string[] TargetPoolObj =
    {
            "Poop_Normal",//기본 똥
            "Golden_Poop", //황금 똥
            "Tissue",//티슈
            "Phone", //폰
            "Paper_Cup", //종이컵
            "Cigarette_Butt",//담배 꽁초
            "Trash_Bag"//쓰레기봉투
    };

    float dropTime;

    public int stage;
    public int score;
    
    int[] score_goul = {100, 200, 300}; 
    public float Player_Hp;

    int BonusObjCount = 0; //보너스 똥 잔여 개수

    // 게임 상태
    public enum GameState { Lobby, Play, Bonus, End }
    // Lobby : 게임 대기, Play : 게임 중
    // Bonus : 스테이지 클리어 - 보너스 스테이지, End : 게임 오버

    public GameState gameState;


    private void Awake()
    {
        instance = this;
        GameInit();
    }

    // 게임 초기화 (게임 시작 시 초기화)
    void GameInit()
    {
        stage = 0;
        gameState = GameState.Play;
        StageInit();
    }

    // 스테이지 초기화 (스테이지 시작 시 초기화)
    void StageInit()
    {
        gameState = GameState.Play;
        stage++;
        score = 0;
        Player_Hp = 100;
        dropTime = 0;
        Background.sprite = BackgroundSprite[stage - 1];
        Text_Score.text = $"{score}";
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.Play:
                dropTime += Time.deltaTime;
                Player_Hp -= Time.deltaTime * 2f;

                // 플레이어가 체력이 다 떨어지면 게임을 끝내도록 조건문 추가
                // 게임 상태를 GameState.End로 변경, return
                if (Player_Hp <= 0)
                {
                    gameState = GameState.End;
                    return;
                }

                if (dropTime >= 1f) Drop();

                HpBar_Fill.fillAmount = Player_Hp / 100;
                Text_Score.text = $"{score}";

                // 스테이지 통과 조건 점수 이상 달성 시 StageClear() 실행
                if(score >= score_goul[stage - 1])
                {
                   StageClear();
                   gameState = GameState.Bonus;
                }
                break;

            case GameState.Bonus:

                Text_Score.text = $"{score}";
                if (BonusObjCount == 0)
                {
                    StageInit();
                }

                break;

            case GameState.End:

                GameOver();
                break;
        }
    }



    // 오브젝트 드롭
    void Drop()
    {
        // TargetPoolObj 내 요소 랜덤 선택
        GameObject dropObject = pool.MakeObject(
            TargetPoolObj[Random.Range(0, TargetPoolObj.Length)]);

        dropObject.transform.position = new Vector2(Random.Range(-2.5f, 2.5f), 6);
        dropObject.SetActive(true);

        dropTime = 0;
    }

    // 스테이지 클리어 시 보너스 스테이지 진행
    void StageClear()
    {
        BonusObjCount = 5;
     
        for (int i = 0; i < 5; i++)
        {
            GameObject bonusObj = pool.MakeObject("Bonus_Poop"); 
            bonusObj.transform.position = new Vector2(Random.Range(-2.5f, 2.5f), 6);
            bonusObj.SetActive(true);
        }
    }

    // 게임 오버
    void GameOver()
    {
        GmaeOverText.gameObject.SetActive(true);
        RestartButton.SetActive(true);
        Time.timeScale = 0f;
    }
    // ReStart버튼 클릭시 게임 다시 초기화
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 게임 재시작 시 게임 초기화
    public void GameRestart() => GameInit();
}
