using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    //public ShopData shopData;
    public int score = 0;
    public int earned = 100;
    public int enemy = 100;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        Debug.Log("onena ble");
       // shopData = DataRuntimeManager.Instance.shopData;
    } 
    private void Start()
    {
        Observer.AddListener(constr.DONELOADLEVEL, InitGame);
        Observer.AddListener(constr.WINGAME, FinishGame);
    }

    private void FinishGame()
    {
        DataRuntimeManager.Instance.dynamicData.NextCurrentIDLevel();
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
