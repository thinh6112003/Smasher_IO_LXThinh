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
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int idWeapon;

    private void Awake()
    {
        Observer.AddListener(constr.CHANGEWEAPON, ChangeWeapon);
    }
    private void Start()
    {
        ChangeWeapon();
    }
    private void ChangeWeapon()
    {
        if (isPlayer)
        {
            idWeapon = DataRuntimeManager.Instance.dynamicData.GetIdWeapon();
        }
        else
        {
            idWeapon = Random.Range(0, listWeapon.Count);
        }
        if(currentWeapon!= null) currentWeapon.gameObject.SetActive(false);
        currentWeapon = listWeapon[idWeapon];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.GetComponent<Weapon>().ChangeWeapon(this);
    }
    public void SetComponentWeapon(GameObject lookdir, GameObject positionhit, GameObject inpuenemy)
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
                SetComponentWeapon(lookDirMace, postionHitMace, inputEnemyMace);
                break;
            case WeaponType.Axe:
                SetComponentWeapon(lookDirAxe, postionHitAxe, inputEnemyAxe);
                break;
            case WeaponType.Hammer:
                SetComponentWeapon(lookDirHammer, postionHitHammer, inputEnemyHammer);
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
