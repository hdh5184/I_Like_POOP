using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PoolManager : MonoBehaviour
{
    /* 
     * 1. 상단에 떨어질 오브젝트 프리펩 가져오기
     * 
     * 예시)
     * public GameObject 똥
     * public GameObject 장애물
     * 
     * 2. 오브젝트를 관리하는 pool 생성
     * 
     * 예시)
     * public GameObject[] pool_똥
     * 
     */



    private void Awake()
    {
        /* 
         * MakeObj 메서드를 이용하여 pool 저장
         * 양식을 사용하여 prefab 별로 개별 생성
         */
    }


    void MakePool(GameObject prefab, ref GameObject[] pool, int count)
    {
        pool = new GameObject[count];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(prefab, transform);
            pool[i].SetActive(false);
        }
    }

    public GameObject MakeObject(string objName)
    {
        GameObject targetPool = null;

        /* 
         * GameManager 등 외부로부터 오브젝트 생성 요청 시 메서드 실행
         * 매개변수 objName을 사용하여 생성할 오브젝트 타겟 지정
         * switch문을 사용하여 구현
         */


        //예시)
        //switch (objName)
        //{
        //    case "object_A": targetPool = object_A의 pool; break;

        //    default: targetPool = null; break;
        //}

        /*
        if (targetPool != null)
        {
            for (int i = 0; i < targetPool.Length; i++)
            {
                if (!targetPool[i].activeSelf)
                {
                    targetPool[i].SetActive(true);
                    return targetPool[i];
                }
            }
        }
        */

        //예외 처리 (Console Log)
        Debug.Log($"\"{objName}\" 오브젝트의 여분이 없거나 찾을 수 없습니다.");
        return null;
    }
}
