using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public virtual void AttackAnimation(Character character)
    {
    }

    public virtual void DeathAnimation(Character character, Transform enemyTf)
    {
    }
    public virtual void ChangeWeapon(WeaponController weaponController)
    {
    }
}
