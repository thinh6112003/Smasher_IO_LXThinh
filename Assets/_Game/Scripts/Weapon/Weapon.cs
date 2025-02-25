using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public virtual void AttackHandle(Character character)
    {
    }
    public virtual void AttackSound()
    {
    }
    public virtual void DeathHandle(Character character, Transform enemyTf)
    {
    }
    public virtual void ChangeWeapon(WeaponController weaponController)
    {
    }
}
