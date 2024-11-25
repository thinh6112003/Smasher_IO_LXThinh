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
        currentIDSelect = DataRuntimeManager.Instance.dynamicData.GetIdWeapon();
        Debug.Log("current id select : "+ currentIDSelect);
        base.Init();
    }
    protected override void OnclickButtonEquip()
    {
        base.OnclickButtonEquip();

        dynamicData.SetIdWeapon(currentIDSelect);
        Observer.Noti(constr.CHANGEWEAPON);
    }
    protected override void SetOpen()
    {
        DataRuntimeManager.Instance.shopData.SetOpenWeapon(currentIDSelect);
    }
    public override void AddItemShopWithType(int i, ref ShopUnit newShopItemUnit)
    {
        Debug.Log("add weapon");
        newShopItemUnit.Init(shopItems[i], DataRuntimeManager.Instance.shopData.GetWeaponStatus(i), i, this, currentIDEquip == i);
    }
}
