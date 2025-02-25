using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using HighlightPlus;

public class Character : MonoBehaviour
{
    public Character target;
    public bool isDie = false;
    public int level= 1;
    public int CountInDeadZone = 0;
    public float scaleValue= 1;
    public Vector3 flatScale= new Vector3(1f,0.05f,1f);
    public Transform parentModel;
    public Collider myCollider;
    [SerializeField] protected float moveSpeed= 7;
    [SerializeField] protected float paramForZoom= 2;
    [SerializeField] protected float paramForScaleSpeed= 5;
    [SerializeField] protected float scaleSpeed= 1;
    [SerializeField] protected string hascode_cua_collider;
    [SerializeField] protected string hascode_cuatoi;
    [SerializeField] protected Animator animCharacter;
    [SerializeField] protected Rigidbody myRigidbody;
    [SerializeField] protected GameObject myParticleSystem;
    [SerializeField] protected Transform Hammer;
    [SerializeField] protected GameObject inputEnemy;
    [SerializeField] protected WeaponController weaponController;
    [SerializeField] protected SkinController skinController;
    protected bool isRun = false;
    protected string currentCharacterAnimName = constr.IDLE;
    protected string currentDeadZoneNotiString = "";
    protected Vector3 localPosModel_Die= new Vector3(0f, 0.08f, 0f);
    protected Vector3 localPosModel_Live= new Vector3(0f, 0.05f, 0f);
    protected CharacterAction lastCharAction= CharacterAction.IDLE;
    protected HighlightEffect myhighlightEffect;
    Vector3 scale1_15 = new Vector3(1.15f,1.15f,1.15f);
    Vector3 scale0_9 = new Vector3(0.9f, 0.9f, 0.9f);
    Vector3 scalezero = new Vector3(0.005f, 0.005f, 0.005f);
    protected virtual void OnMove()
    {
    }
    protected virtual void Start()
    {
        inputEnemy.SetActive(false);
        Observer.AddListener(constr.ATTACK + gameObject.GetHashCode(), ActiveInputEnemy);
        weaponController.SetWeapon(WeaponController.WeaponType.Axe);
        myhighlightEffect = GetComponent<HighlightEffect>();
    }
    protected virtual void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == constr.CHARACTER)
        {
            level+= other.GetComponent<Character>().level;
            other.GetComponent<Character>().Die(weaponController.currentWeapon, transform);
            ZoomUpEffect();
        }
    }
    protected virtual void Init()
    {
        level = 1;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        parentModel.localPosition = localPosModel_Live;
        parentModel.localScale = Vector3.one;
        parentModel.localRotation = Quaternion.Euler(0, 0, 0);
        SetAnimation(AnimationType.IDLE);
        transform.localScale = Vector3.one;
        isDie = false;
        lastCharAction = CharacterAction.IDLE;
        Hammer.gameObject.SetActive(true);
        myCollider.enabled = true;
        myRigidbody.isKinematic = false;
        scaleValue = 1;
        scaleSpeed = 1;
    }
    protected virtual void Attack()
    {
        if (!isDie && lastCharAction == CharacterAction.RUN)
        {
            isRun = false;
            lastCharAction = CharacterAction.ATTACK;
            weaponController.currentWeapon.AttackHandle(this);
            Observer.AddListener(constr.IDLE + gameObject.GetHashCode(), resetMove);
        }
    }
    protected virtual async void ActiveInputEnemy()
    {
        inputEnemy.SetActive(true);
        await Task.Delay((int)(Time.deltaTime * 2000));
        inputEnemy.SetActive(false);
    }
    protected virtual async void Die(Weapon weaponhit,Transform enemyTf)
    {
        weaponhit.DeathHandle(this,enemyTf);
        myParticleSystem.SetActive(true);
        Hammer.gameObject.SetActive(false);
        parentModel.transform.localPosition = localPosModel_Die;
        GetComponent<Collider>().enabled = false;
        myRigidbody.isKinematic = true;
        isDie = true;
        await Task.Delay(2000);
        parentModel.DOScale(scalezero, 1.4f).SetEase(Ease.InBack);
    }
    protected virtual async void ZoomUpEffect()
    {
        await Task.Delay(350);
        myhighlightEffect.HitFX();
        Observer.Noti(constr.LEVELUP);
        CountInDeadZone = 0;
        scaleValue = ((float)level - 1) / paramForZoom + 1f;
        scaleSpeed = ((float)level - 1) / paramForScaleSpeed + 1f;
        transform.DOScale(scaleValue * scale1_15, 0.2f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                /// fixx
                transform.DOScale(scaleValue * scale0_9, 0.15f).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    transform.DOScale(scaleValue * Vector3.one, 0.15f).SetEase(Ease.OutSine);
                });
            });
    }
    protected virtual void Run(Vector3 moveVector)
    {        
        bool tmp = isRun;
        isRun = true;
        Observer.Noti(constr.RUN + gameObject.GetHashCode());
        lastCharAction = CharacterAction.RUN;
        SetAnimation(AnimationType.RUN);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), 0.3f);
    }
    public void resetMove()
    {
        SetAnimation(AnimationType.IDLE);
    }
    public void ChangeSkin(int id)
    {

    }
    public void ChangeWepon(int id)
    {

    }
    public void ChangeAnim(string animName)
    {
        if (currentCharacterAnimName != animName)
        {
            animCharacter.ResetTrigger(currentCharacterAnimName);
            currentCharacterAnimName = animName;
            animCharacter.SetTrigger(currentCharacterAnimName);
        }
    }
    public void SetAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.ATTACKHAMMER:
                ChangeAnim(constr.ATTACKHAMMER);
                break;
            case AnimationType.ATTACKAXE:
                ChangeAnim(constr.ATTACKAXE);
                break;
            case AnimationType.ATTACKMACE:
                ChangeAnim(constr.ATTACKMACE);
                break;
            case AnimationType.IDLE:
                animCharacter.SetFloat("MoveBase", 0);
                ChangeAnim(constr.RUN_IDLE);
                break;
            case AnimationType.DIEMACE:
                ChangeAnim(constr.DIEMACE);
                break;
            case AnimationType.RUN:
                animCharacter.SetFloat("MoveBase", 1);
                ChangeAnim(constr.RUN_IDLE);
                break;
            case AnimationType.DIE:
                ChangeAnim(constr.DIE);
                break;
            case AnimationType.ANMUNG:
                ChangeAnim(constr.ANMUNG);
                break;
            case AnimationType.TOIDLE:
                animCharacter.SetFloat("MoveBase", 0);
                ChangeAnim(constr.RUN_IDLE);
                break;
        }
    }
}

public enum CharacterAction { IDLE, RUN, ATTACK, DIE }
public enum AnimationType
{
    RUN,
    IDLE,

    DIE,
    DIEMACE,

    ATTACKHAMMER,
    ATTACKMACE,
    ATTACKAXE,

    ANMUNG,
    TOIDLE
}