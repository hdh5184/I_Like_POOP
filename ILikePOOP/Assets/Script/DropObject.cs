using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    GameManager gm; //게임 관리
    
    // 오브젝트 데이터
    public int ObjScore;
    public int ObjHp;
    public int Speed = 3;



    // 오브젝트 타입
    public enum DropObjectType
    {
        Normal_Poop,
        Golden_Poop,
        Bonus_Poop,
        Diamond_Poop,

        //장애물   
        Tissue,
        Phone,
        Paper_Cup,
        Cigarette_Butt,
        Trash_Bag

        /* 
         * 상단에서 떨어지는 오브젝트 타입 모음
         * 똥, 장애물 별 개별 구성
         * 보너스 똥 포함
         * Component에 오브젝트 별 dropObjectType 지정
         * 
         * 예시)
         * Poop_Normal, Tissue, ...
         * 
         * 참고 : 보너스 각진 똥은 바닥에 꺼지지 않도록 함
         */
    }
    public DropObjectType dropObjectType;

    // 오브젝트 초기 설정
    private void Awake()
    {
        gm = GameManager.instance;

        switch (dropObjectType)
        {
            /* 수정 사항
             * 
             * GameManager의 score 및 Player_Hp에 변동을 적용하는 것이 아닌
             * ObjScore 및 ObjHp에 점수와 체력을 저장하는 것으로 수정하기
             * 
             * 플레이어가 똥 획득 시 점수와 체력 증가
             * 플레이어가 장애물 획득 시 체력만 감소하도록 설정
             * 
             */


            case DropObjectType.Normal_Poop:
                ObjScore += 5;
                ObjHp += 5;
                break;

            case DropObjectType.Golden_Poop:
                ObjScore += 10;
                ObjHp += 10;
                break;

            case DropObjectType.Bonus_Poop:
                ObjScore += 15;
                ObjHp += 15;
                break;

            case DropObjectType.Diamond_Poop:
                ObjScore += 15;
                ObjHp += 15;
                break;

            case DropObjectType.Tissue:
                ObjHp -= 5;
                break;

            case DropObjectType.Phone:
                ObjHp -= 5;
                break;

            case DropObjectType.Paper_Cup:
                ObjHp -= 10;
                break;

            case DropObjectType.Trash_Bag:
                ObjHp -= 10;
                break;

            case DropObjectType.Cigarette_Butt:
                ObjHp -= 15;
                break;

        }
    }


    void Update()
    {
        // 보너스 똥은 이동 제외 (중력 적용)
        //transform.Translate(Vector2.down * Speed * Time.deltaTime);
        Vector2 pos = transform.position;
        transform.position = new Vector2(
            Mathf.Clamp(pos.x, -2.5f, 2.5f), pos.y);
    }
}
