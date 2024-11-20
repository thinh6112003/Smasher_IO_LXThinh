using Sirenix.OdinInspector;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Edit Tools/Dynamic Data Editor", fileName = "Dynamic Data Editor", order = 0)]
public class DynamicDataEditorSO : ScriptableObject
{
    public DynamicData dynamicData;

    [ButtonGroup("dynamicData")]
    public void SaveUserData()
    {
        if (EditorUtility.DisplayDialog("Save Dynamic Data", "Are you sure you wanna save this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.SaveData(dynamicData, DataRuntimeManager.DYNAMIC_DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
        }
    }
    [ButtonGroup("dynamicData")]
    public void ReloadUserData()
    {
        dynamicData = SimpleDataSave.LoadData<DynamicData>(Path.Combine(DataRuntimeManager.DataPersistentDirectoryPath, DataRuntimeManager.DYNAMIC_DATA_RUNTIME_FILE_NAME));
    }
    [ButtonGroup("dynamicData")]
    public void DeleteUserData()
    {
        if (EditorUtility.DisplayDialog("Delete Dynamic Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<DynamicData>(DataRuntimeManager.DYNAMIC_DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
            dynamicData = new DynamicData();
        }
    }


    public ShopData shopData;

    [ButtonGroup("shopData")]
    public void SaveShopData()
    {
        if (EditorUtility.DisplayDialog("Save Shop Data", "Are you sure you wanna save this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.SaveData(shopData, DataRuntimeManager.SHOP_DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
        }
    }
    [ButtonGroup("shopData")]
    public void ReloadShopData()
    {
        shopData = SimpleDataSave.LoadData<ShopData>(Path.Combine(DataRuntimeManager.DataPersistentDirectoryPath, DataRuntimeManager.SHOP_DATA_RUNTIME_FILE_NAME));
    }
    [ButtonGroup("shopData")]
    public void DeleteShopData()
    {
        if (EditorUtility.DisplayDialog("Delete Shop Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<ShopData>(DataRuntimeManager.SHOP_DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
            shopData = new ShopData();
        }
    }
}
#endif