using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponShopSO", menuName = "ShopSO/WeaponShopSO")]
public class WeaponShopSo : ScriptableObject
{
    public List<WeaponShopItem> weaponShopItems;
}
