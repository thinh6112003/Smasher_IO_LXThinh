using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public DynamicData dynamicData;
    public ShopData shopData;
    public int score = 0;
    public int earned = 100;
    public int enemy = 100;
    private const string keyGetSetData = "DynamicData";
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        dynamicData = DataRuntimeManager.Instance.dynamicData;
        shopData = DataRuntimeManager.Instance.shopData;
    } 
    private void Start()
    {
        Observer.AddListener(constr.DONELOADLEVEL, InitGame);
        Observer.AddListener(constr.WINGAME, FinishGame);
        Observer.AddListener(constr.NEXTLEVEL, () =>
        {
            dynamicData.AddMonney(earned);
        });
    }

    private void FinishGame()
    {
        dynamicData.NextCurrentIDLevel();
    }

    private void UpdateScore()
    {
        score++;
        Observer.Noti(constr.UPDATEUI);
    }
    public int GetScore()
    {
        return score;
    }
    public void IncScore()
    {
        score++;
        Observer.Noti(constr.UPDATEUI);
    }

    private void InitGame()
    {
        score = 0;
        Observer.Noti(constr.UPDATEUI);
    }
    public int GetEarned()
    {
        return earned;
    }
}
