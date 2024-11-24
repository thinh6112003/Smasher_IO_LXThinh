using UnityEngine;

public class ShopSkin : Shop
{
    [SerializeField] private GameObject skinBase;
    [SerializeField] private SkinShopSO skinShopSO;

    protected override void Init()
    {
        for (int i = 0; i < skinShopSO.skinShopItems.Count; i++)
        {
            shopItems.Add(skinShopSO.skinShopItems[i]);
        }
        Debug.Log("shopskin");
        currentIDSelect = DataRuntimeManager.Instance.dynamicData.GetIdSkin();
        base.Init();
    }
    protected override void OnclickButtonEquip()
    {
        base.OnclickButtonEquip();

        dynamicData.SetIdSkin(currentIDSelect);
        Observer.Noti(constr.CHANGESKIN);
    }
    public override void ChangeSelectByID(int id)
    {
        base.ChangeSelectByID(id);
        if (skinShopSO.skinShopItems[id].style == Style.HEROA)
            skinBase.SetActive(true);
        else
            skinBase.SetActive(false);
    }
    protected override void SetOpen()
    {
        DataRuntimeManager.Instance.shopData.SetOpenSkin(currentIDSelect);
    }
}