using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    GameManager gm; //게임 관리

    public DropObjectType dropObjectType;

    public int ObjScore;
    public int ObjHp;
    public int Speed = 3;

    public enum DropObjectType
    {
        /* 
         * 상단에서 떨어지는 오브젝트 타입 모음
         * 똥, 장애물 별 개별 구성
         * 보너스 똥 포함
         * Component에 오브젝트 별 dropObjectType 지정
         * 
         * 예시)
         * Poop_Normal, Poop_Gold, ...
         * 
         * 참고 : 보너스 각진 똥은 바닥에 꺼지지 않도록 함
         */
    }

    private void Awake()
    {
        gm = GameManager.instance;

        /* 
         * dropObjectType별 플레이어 충돌 시 적용 점수 및 체력 개별 설정
         * switch문을 사용하여 구현
         */

    }

    void Update()
    {
        // 보너스 똥은 이동 제외 (중력 적용)
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }
}
