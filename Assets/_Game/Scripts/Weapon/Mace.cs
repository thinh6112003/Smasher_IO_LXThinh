using UnityEngine;

public class Mace : Weapon
{
    public override void AttackHandle(Character character)
    {
        character.SetAnimation(AnimationType.ATTACKMACE);
    }
    public override void DeathHandle(Character character, Transform enemyTf)
    {
        character.SetAnimation(AnimationType.DIEMACE);
        character.parentModel.LookAt(enemyTf);
    }
    public override void ChangeWeapon(WeaponController weaponController)
    {
        weaponController.SetWeapon(WeaponController.WeaponType.Mace);
    }
}
