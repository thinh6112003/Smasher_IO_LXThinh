using DG.Tweening;
using UnityEngine;

public class Hammer : Weapon
{
    public override void AttackHandle(Character character)
    {
        character.SetAnimation(AnimationType.ATTACKHAMMER);
    }
    public override void AttackSound()
    {
        AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
    }
    public override void DeathHandle(Character character, Transform enemyTf)
    {
        character.SetAnimation(AnimationType.DIE);
        character.parentModel.DOScale(character.flatScale, 0.5f);
    }
    public override void ChangeWeapon(WeaponController weaponController)
    {
        weaponController.SetWeapon(WeaponController.WeaponType.Hammer);
    }
}
