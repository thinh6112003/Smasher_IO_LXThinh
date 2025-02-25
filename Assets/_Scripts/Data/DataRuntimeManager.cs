using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataRuntimeManager : KBTemplate.Patterns.Singleton.Singleton<DataRuntimeManager>
{
    public static string DataPersistentDirectoryPath => Application.persistentDataPath + "/DT";
    public readonly static string DYNAMIC_DATA_RUNTIME_FILE_NAME = "DNM_DT.ngm";
    public readonly static string SHOP_DATA_RUNTIME_FILE_NAME = "SHP_DT.ngm";

    [SerializeField] private SaveGameSO defaultSaveGameFile;
    public DynamicData dynamicData { get; private set; }
    public ShopData shopData { get; private set; }
    public bool vibrationStatus = true;
    public bool soundStatus = true;
    public override void OnCreatedSingleton()
    {
        base.OnCreatedSingleton();
        DontDestroyOnLoad(this);
        Init();
    }
    private void Init()
    {
        LoadShopDataRuntime();
        LoadDynamicDataRuntime();
        Observer.AddListener(constr.WINGAME, FinishGame);
    }

    #region shop data
    private void LoadShopDataRuntime()
    {
        shopData = SimpleDataSave.LoadData<ShopData>(System.IO.Path.Combine(DataPersistentDirectoryPath, SHOP_DATA_RUNTIME_FILE_NAME));
        if (shopData == null)
        {
            if (defaultSaveGameFile)
                shopData = defaultSaveGameFile.shopData.DeepCopy();
            else
            {
                Debug.Log("defaultSaveGameFile is null");
                shopData = new ShopData();
            }
        }
    }
    #endregion

    #region dynamic data
    private void LoadDynamicDataRuntime()
    {
        dynamicData = SimpleDataSave.LoadData<DynamicData>(System.IO.Path.Combine(DataPersistentDirectoryPath, DYNAMIC_DATA_RUNTIME_FILE_NAME));
        if (dynamicData == null)
        {
            if (defaultSaveGameFile)
                dynamicData = defaultSaveGameFile.dynamicData.DeepCopy();
            else {
                Debug.Log("defaultSaveGameFile is null");
                dynamicData = new DynamicData();    
            }
        }
    }
    #endregion
    private void SaveDataRuntime()
    {
        SimpleDataSave.SaveData(dynamicData, DYNAMIC_DATA_RUNTIME_FILE_NAME, DataPersistentDirectoryPath);
        SimpleDataSave.SaveData(shopData, SHOP_DATA_RUNTIME_FILE_NAME, DataPersistentDirectoryPath);
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveDataRuntime();
        }
    }
    private void FinishGame()
    {
        dynamicData.NextCurrentIDLevel();
    }

}
