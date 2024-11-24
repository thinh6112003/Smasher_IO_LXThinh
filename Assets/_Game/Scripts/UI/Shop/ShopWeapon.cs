using UnityEngine;

public class ShopWeapon : Shop
{
    [SerializeField] private WeaponShopSo weaponShopSO;
    protected override void Init()
    {
        for (int i = 0; i < weaponShopSO.weaponShopItems.Count; i++)
        {
            shopItems.Add(weaponShopSO.weaponShopItems[i]);
        }
        Debug.Log("shopskin");
        currentIDSelect = DataRuntimeManager.Instance.dynamicData.GetIdWeapon();
        Debug.Log("current id select : "+ currentIDSelect);
        base.Init();
    }
    protected override void OnclickButtonEquip()
    {
        base.OnclickButtonEquip();

        dynamicData.SetIdWeapon(currentIDSelect);
        Observer.Noti(constr.CHANGESKIN);
    }
    protected override void SetOpen()
    {
        DataRuntimeManager.Instance.shopData.SetOpenWeapon(currentIDSelect);
    }
}
