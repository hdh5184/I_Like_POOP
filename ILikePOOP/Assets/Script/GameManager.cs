using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;

    public TextMeshProUGUI Text_Score;
    public Image HpBar_Fill;

    public Sprite[] BackgroundSprite;
    public SpriteRenderer Background;

    public GameObject Player;

    string[] TargetPoolObj =
    {
        //pool에서 생성할 오브젝트 이름 모음
        "test"
    };

    float dropTime;

    public int stage;
    public int score;
    public float Player_Hp;

    int BonusObjCount = 0;

    public enum GameState { Lobby, Play, Bonus, End }
    // Lobby : 게임 대기, Play : 게임 중
    // Bonus : 스테이지 클리어 - 보너스 스테이지, End : 게임 오버

    public GameState gameState;

    private void Awake()
    {
        instance = this;
        gameState = GameState.Lobby;
    }

    // 게임 초기화 (게임 시작 시 초기화)
    void GameInit()
    {
        stage = 0;
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
        Background.sprite = BackgroundSprite[stage];
        Text_Score.text = $"{score}";
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.Play:
                dropTime += Time.deltaTime;
                Player_Hp -= Time.deltaTime * 2f;

                if (dropTime >= 1f) Drop();

                HpBar_Fill.fillAmount = Player_Hp / 100;
                Text_Score.text = $"{score}";

                // 스테이지 통과 조건 점수 이상 달성 시 StageClear() 실행

                break;

            case GameState.Bonus:

                // 보너스 스테이지
                // 플레이어 체력 감소 없음
                // 보너스 똥 이외 생성 안함
                // 점수 표시 유지 (보너스 똥 점수 +)
                // 보너스 똥을 모두 획득 시 StageInit() 실행

                break;

            case GameState.End:
                
                break;
        }
    }



    // 오브젝트 드롭
    void Drop()
    {
        GameObject dropObject =
                pool.MakeObject(TargetPoolObj[Random.Range(0, TargetPoolObj.Length)]);

        dropObject.transform.position = new Vector2(Random.Range(-2.5f, 2.5f), 6);
        dropObject.SetActive(true);

        dropTime = 0;
    }

    // 스테이지 클리어 시 보너스 스테이지 진행
    void StageClear()
    {
        BonusObjCount = 5;
        /* 보너스 각진 똥이 5개 떨어짐
         * pool을 사용하여 보너스 각진 똥 5개 생성
         */
    }

    void GameOver()
    {
        // 플레이어 죽은 스테이지에서 점수 체크하고 다시 시작 버튼 출력
    }

    public void GameRestart() => GameInit();
}
