using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopUnit : MonoBehaviour
{
    WeaponShopItem weaponShopItem;
    [SerializeField] private GameObject equipIcon;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;
    ShopWeapon shopWeapon;
    int id;
    bool isOpen;
    public void Init(WeaponShopItem shopItemToSet, bool status, int idToSet, ShopWeapon shopWeaponToSet)
    {

        id = idToSet;
        shopWeapon = shopWeaponToSet;
        weaponShopItem = shopItemToSet;
        if (status)
        {
            iconImage.sprite = weaponShopItem.spriteOpen;
        }
        else
        {
            iconImage.sprite = weaponShopItem.spriteLock;
        }
        button.onClick.AddListener(() => shopWeapon.ChangeSkinByID(idToSet));
        Debug.Log(id);
    }
    public void SetEquiped()
    {
        equipIcon.SetActive(true);
        Debug.Log("set equiped " + id);
    }
    public void SetUnEquiped()
    {
        Debug.Log("not equiped " + id);
        equipIcon.SetActive(false);
    }
    public void SetOpen()
    {
        iconImage.sprite = weaponShopItem.spriteOpen;
    }
}
