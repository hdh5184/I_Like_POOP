using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PoolManager : MonoBehaviour
{
    public GameObject
        prefab_Poop_Normal,
        prefab_Poop_Golden,
        prefab_Poop_Bonus,
        prefab_Tissue,
        prefab_Phone,
        prefab_Paper_Cup,
        prefab_Cigarette_Butt,
        prefab_Trash_Bag;

    GameObject[]
        pool_Poop_Normal,
        pool_Poop_Golden,
        pool_Poop_Bouns,
        pool_Tissue,
        pool_Phone,
        pool_Paper_Cup,
        pool_Cigarette_Butt,
        pool_Trash_Bag;

    private void Awake()
    {
        /*
        MakePool(prefab_Poop_Normal, ref pool_Poop_Normal, 20);
        MakePool(prefab_Poop_Golden, ref pool_Poop_Golden, 20);
        MakePool(prefab_Poop_Bonus, ref pool_Poop_Bouns, 20);
        MakePool(prefab_Tissue, ref pool_Tissue, 20);
        MakePool(prefab_Phone, ref pool_Phone, 20);
        MakePool(prefab_Paper_Cup, ref pool_Paper_Cup, 20);
        MakePool(prefab_Cigarette_Butt, ref pool_Cigarette_Butt, 20);
        MakePool(prefab_Trash_Bag, ref pool_Trash_Bag, 20);
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
        GameObject[] targetPool = null;

        switch (objName)
        {
            case "Poop_Normal":     targetPool = pool_Poop_Normal; break;
            case "Golden_Poop":     targetPool = pool_Poop_Golden; break;
            case "Bonus_Poop":      targetPool = pool_Poop_Bouns; break;
            case "Tissue":          targetPool = pool_Tissue; break;
            case "Phone":           targetPool = pool_Phone; break;
            case "Paper_Cup":       targetPool = pool_Paper_Cup; break;
            case "Cigarette_Butt":  targetPool = pool_Cigarette_Butt; break;
            case "Trash_Bag":       targetPool = pool_Trash_Bag; break;

            default: targetPool = null; break;
        }

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

        //예외 처리
        Debug.Log($"\"{objName}\" 오브젝트의 여분이 없거나 찾을 수 없습니다.");
        return null;
    }
}
