using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{    
    [SerializeField] private bool testChangeWeapon1;
    [SerializeField] private bool testChangeWeapon5;
    [SerializeField] private bool testChangeWeapon15; 
    [SerializeField] private bool isPlayer= false; 
    [SerializeField] private List<Weapon> listWeapon = new List<Weapon>();
    [SerializeField] internal Weapon currentWeapon;
    [SerializeField] private GameObject inputEnemy;
    [SerializeField] private GameObject inputEnemyMace;
    [SerializeField] private GameObject inputEnemyAxe;
    [SerializeField] private GameObject inputEnemyHammer;
    [SerializeField] private GameObject positionHit;
    [SerializeField] private GameObject postionHitAxe;
    [SerializeField] private GameObject postionHitHammer;
    [SerializeField] private GameObject postionHitMace;
    [SerializeField] private GameObject lookDir;
    [SerializeField] private GameObject lookDirMace;
    [SerializeField] private GameObject lookDirAxe;
    [SerializeField] private GameObject lookDirHammer;

    private void Update()
    {
        if(testChangeWeapon1)
        {
            testChangeWeapon1 = false;
            ChangeWeapon(1);
        }
        if (testChangeWeapon5)
        {
            testChangeWeapon5 = false;
            ChangeWeapon(5);
        }
        if (testChangeWeapon15)
        {
            testChangeWeapon15 = false;
            ChangeWeapon(15);
        }
    }
    public void ChangeWeapon(int id)
    {
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = listWeapon[id];
        currentWeapon.gameObject.SetActive(true);
        listWeapon[id].ChangeWeapon(this);
    } 
    public void ChangeWeapon(GameObject lookdir, GameObject positionhit, GameObject inpuenemy)
    {
        if(isPlayer&& lookDir != null)
            lookDir.SetActive(false);
        lookdir.SetActive(true);

        if(positionHit!= null)
            positionHit.SetActive(false);
        positionhit.SetActive(true);

        if (inputEnemy!= null) 
                inputEnemy.SetActive(false);
        inpuenemy.SetActive(true);

        lookDir = lookdir;
        positionHit = positionhit;
        inputEnemy = inpuenemy;
    }
    public void SetWeapon(WeaponType weaponType)
    {
        switch (weaponType) {
            case WeaponType.Mace:
                ChangeWeapon(lookDirMace, postionHitMace, inputEnemyMace);
                break;
            case WeaponType.Axe:
                ChangeWeapon(lookDirAxe, postionHitAxe, inputEnemyAxe);
                break;
            case WeaponType.Hammer:
                ChangeWeapon(lookDirHammer, postionHitHammer, inputEnemyHammer);
                break;
        }
    }
    public enum WeaponType
    {
        Mace,
        Axe,
        Hammer
    }
}
