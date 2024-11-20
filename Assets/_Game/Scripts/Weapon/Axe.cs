using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public override void AttackAnimation(Character character)
    {
        character.SetAnimation(AnimationType.ATTACKAXE);
    }

    public override void DeathAnimation(Character character, Transform enemyTf)
    {
        character.SetAnimation(AnimationType.DIEMACE);
        character.parentModel.LookAt(enemyTf);
    }
    public override void ChangeWeapon(WeaponController weaponController)
    {
        weaponController.SetWeapon(WeaponController.WeaponType.Axe);
    }
}
