using System;
using System.Collections.Generic;

[Serializable]

public class ShopData
{
    public int countOfSkins;
    public int countOfWeapons;
    public List<bool> skinsStatus;
    public List<bool> weaponStatus;

    public ShopData()
    {
        countOfSkins = 27;
        countOfWeapons = 27;
        skinsStatus = new List<bool>();
        SetAllSkins();
        skinsStatus[0] = true;
        weaponStatus = new List<bool>();
        SetAllWeapons();
        weaponStatus[4] = true;
    }
    public void SetOpenSkin(int id)
    {
        skinsStatus[id] = true;
    }
    public void SetOpenWeapon(int id)
    {
        weaponStatus[id] = true;
    }
    public bool GetSkinStatus(int id)
    {
        return skinsStatus[id];
    }
    public bool GetWeaponStatus(int id)
    {
        return weaponStatus[id];
    }
    public void SetAllSkins()
    {
        for (int i = 0; i < countOfSkins; i++)
        {
            skinsStatus.Add(false);
        }
    }
    public void SetAllWeapons()
    {
        for (int i = 0; i < countOfWeapons; i++)
        {
            weaponStatus.Add(false);
        }
    }
    internal ShopData DeepCopy()
    {
        return new ShopData
        {
            countOfSkins = this.countOfSkins,
            countOfWeapons = this.countOfWeapons,
            skinsStatus = new List<bool>(this.skinsStatus),
            weaponStatus = new List<bool>(this.weaponStatus)
        };
    }
}
