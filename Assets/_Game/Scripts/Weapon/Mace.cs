using UnityEngine;

public class Mace : Weapon
{
    public override void AttackAnimation(Character character)
    {
        character.SetAnimation(AnimationType.ATTACKMACE);
    }

    public override void DeathAnimation(Character character, Transform enemyTf)
    {
        character.SetAnimation(AnimationType.DIEMACE);
        character.parentModel.LookAt(enemyTf);
    }
    public override void ChangeWeapon(WeaponController weaponController)
    {
        weaponController.SetWeapon(WeaponController.WeaponType.Mace);
    }
}
