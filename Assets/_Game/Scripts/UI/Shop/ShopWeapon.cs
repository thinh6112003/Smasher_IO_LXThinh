using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private WeaponShopSo weaponShopSO;
    [SerializeField] private WeaponShopUnit weaponShopUnit;
    [SerializeField] private Transform raceContentTf;
    [SerializeField] private Transform commonContentTf;
    [SerializeField] private Transform epicContentTf;
    [SerializeField] private RectTransform rareContentRectTf;
    [SerializeField] private RectTransform commonContentRectTf;
    [SerializeField] private RectTransform epicContentRectTf;
    [SerializeField] private Button buttonRare;
    [SerializeField] private Button buttonCommon;
    [SerializeField] private Button buttonEpic;
    [SerializeField] private int currentIDWeapon;
    int countRaRe = 0;
    int countEpic = 0;
    int countCommon = 0;
    private List<WeaponShopUnit> weaponShopUnits = new List<WeaponShopUnit>();
    private void Start()
    {
        //for (int i = 0; i < weaponShopSO.weaponShopItems.Count; i++)
        //{
        //    WeaponShopItem newSkinShopItem = weaponShopSO.weaponShopItems[i];
        //    switch (newSkinShopItem.type)
        //    {
        //        case Type.RARE:
        //            AddItemShop(rareContentRectTf, raceContentTf, ref countRaRe, i);
        //            break;
        //        case Type.EPIC:
        //            AddItemShop(epicContentRectTf, epicContentTf, ref countEpic, i);
        //            break;
        //        case Type.COMMON:
        //            AddItemShop(commonContentRectTf, commonContentTf, ref countCommon, i);
        //            break;
        //    }
        //}
        //currentIDWeapon = DataManager.Instance.dynamicData.GetIdSkin();
        //ChangeSkinByID(currentIDWeapon);
        //weaponShopUnits[currentIDWeapon].SetEquiped();
        //buttonCommon.onClick.AddListener(OnclickButtonCommon);
        //buttonEpic.onClick.AddListener(OnclickButtonEpic);
        //buttonRare.onClick.AddListener(OnclickButtonRare);
    }

    private void OnclickButtonRare()
    {
        raceContentTf.gameObject.SetActive(true);
        epicContentTf.gameObject.SetActive(false);
        commonContentTf.gameObject.SetActive(false);
    }

    private void OnclickButtonEpic()
    {
        epicContentTf.gameObject.SetActive(true);
        commonContentTf.gameObject.SetActive(false);
        rareContentRectTf.gameObject.SetActive(false);
    }

    private void OnclickButtonCommon()
    {
        commonContentTf.gameObject.SetActive(true);
        epicContentTf.gameObject.SetActive(false);
        rareContentRectTf.gameObject.SetActive(false);
    }
    public void AddItemShop(RectTransform contentRectTransform, Transform contenTransform, ref int count, int i)
    {
        count++;
        if (count % 3 == 1 && count > 1)
        {
            contentRectTransform.sizeDelta += new Vector2(0, 54 + 200f);
        }
        WeaponShopUnit newWeaponUnit = Instantiate(weaponShopUnit, contenTransform);
        newWeaponUnit.Init(weaponShopSO.weaponShopItems[i], DataManager.Instance.shopData.GetSkinStatus(i), i, this);
        weaponShopUnits.Add(newWeaponUnit);
    }
    public void ChangeSkinByID(int id)
    {

        weapons[currentIDWeapon].SetActive(false);
        weaponShopUnits[currentIDWeapon].SetUnEquiped();
        currentIDWeapon = id;

        weapons[currentIDWeapon].SetActive(true);

        weaponShopUnits[currentIDWeapon].SetEquiped();
        DataManager.Instance.dynamicData.SetIdSkin(currentIDWeapon);
        Observer.Noti(constr.CHANGESKIN);
        Debug.Log("change skin: " + id);
    }
}
