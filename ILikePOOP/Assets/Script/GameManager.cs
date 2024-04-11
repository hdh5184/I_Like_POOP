using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 관리
    public static GameManager instance;
    public PoolManager pool;

    // UI
    public TextMeshProUGUI Text_Score;
    public Image HpBar_Fill;

    // Background 스프라이트 저장
    public Sprite[] BackgroundSprite;
    public SpriteRenderer Background;

    public GameObject Player;

    // 오브젝트 생성 타겟 (랜덤)
    string[] TargetPoolObj =
    {
        //pool에서 생성할 오브젝트 이름 모음
        //보너스 똥을 제외한 나머지 똥 및 장애물 전부 
        //예시 : "Poop_Normal", "Tissue", ....
    };

    float dropTime;

    public int stage;
    public int score;
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
        /* 보너스 각진 똥이 5개 떨어짐
         * pool을 사용하여 보너스 각진 똥 5개 생성
         * MakeObject(<보너스 똥 문자열>);
         */
    }

    // 게임 오버
    void GameOver()
    {
        // 플레이어 죽은 스테이지에서 점수 체크하고 다시 시작 버튼 출력
    }

    // 게임 재시작 시 게임 초기화
    public void GameRestart() => GameInit();
}
