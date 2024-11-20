using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DynamicData
{
    public int currentIDLevel;
    public int currentMonney;
    public int idSkin;
    public int idWeapon;
    public bool vibrationStatus;
    public bool soundStatus;

    public DynamicData()
    {
        currentIDLevel = 1;
        currentMonney = 100;
        idSkin = 1;
        idWeapon = 1;
        soundStatus = true;
        vibrationStatus = true;
    }

    // Level related methods
    public void SetCurrentIDLevel(int iDLevel)
    {
        currentIDLevel = iDLevel;
    }

    public int GetCurrentIDLevel()
    {
        return currentIDLevel;
    }

    public int NextCurrentIDLevel()
    {
        currentIDLevel++;
        if (currentIDLevel > 5)
        {
            currentIDLevel = 1;
        }
        return currentIDLevel;
    }

    public string CurrentEnviromentSceneName()
    {
        return "Map " + currentIDLevel.ToString();
    }
    public void SetCurrentMonney(int coin)
    {
        currentMonney = coin;
    }

    public int GetCurrentMonney()
    {
        return currentMonney;
    }

    public void AddMonney(int amount)
    {
        currentMonney += amount;
        Observer.Noti(constr.UPDATEUI);
    }

    public void SubtractMonney(int amount)
    {
        currentMonney -= amount;
        Observer.Noti(constr.UPDATEUI);
    }

    public bool HasEnoughMonney(int amount)
    {
        return currentMonney >= amount;
    }

    // Skin related methods
    public void SetIdSkin(int id)
    {
        idSkin = id;
    }

    public int GetIdSkin()
    {
        return idSkin;
    }

    // Weapon related methods
    public void SetIdWeapon(int id)
    {
        idWeapon = id;
    }

    public int GetIdWeapon()
    {
        return idWeapon;
    }

    // Sound and vibration related methods
    public void SetActiveSoundStatus(bool status)
    {
        soundStatus = status;
    }

    public bool GetSoundStatus()
    {
        return soundStatus;
    }

    public void SetVibrationStatus(bool status)
    {
        vibrationStatus = status;
    }

    public bool GetVibrationStatus()
    {
        return vibrationStatus;
    }

    // Deep copy method
    internal DynamicData DeepCopy()
    {
        return new DynamicData
        {
            currentIDLevel = this.currentIDLevel,
            currentMonney = this.currentMonney,
            idSkin = this.idSkin,
            idWeapon = this.idWeapon,
            soundStatus = this.soundStatus,
            vibrationStatus = this.vibrationStatus
        };
    }
}
