using UnityEngine;

public class Axe : Weapon
{
    public override void AttackHandle(Character character)
    {
        character.SetAnimation(AnimationType.ATTACKAXE);
    }
    public override void AttackSound()
    {
        AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
    }
    public override void DeathHandle(Character character, Transform enemyTf)
    {
        character.SetAnimation(AnimationType.DIEMACE);
        character.parentModel.LookAt(enemyTf);
    }
    public override void ChangeWeapon(WeaponController weaponController)
    {
        weaponController.SetWeapon(WeaponController.WeaponType.Axe);
    }
}
