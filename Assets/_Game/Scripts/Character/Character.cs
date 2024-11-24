using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

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
    protected bool isRun = false;
    protected string currentCharacterAnimName = constr.IDLE;
    protected string currentDeadZoneNotiString = "";
    protected Vector3 localPosModel_Die= new Vector3(0f, 0.08f, 0f);
    protected Vector3 localPosModel_Live= new Vector3(0f, 0.05f, 0f);
    protected CharacterAction lastCharAction= CharacterAction.IDLE;
    protected virtual void OnMove()
    {
    }
    protected virtual void Start()
    {
        inputEnemy.SetActive(false);
        Observer.AddListener(constr.ATTACK + gameObject.GetHashCode(), ActiveInputEnemy);
        weaponController.SetWeapon(WeaponController.WeaponType.Axe);
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
            weaponController.currentWeapon.AttackAnimation(this);
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
        weaponhit.DeathAnimation(this,enemyTf);
        myParticleSystem.SetActive(true);
        Hammer.gameObject.SetActive(false);
        parentModel.transform.localPosition = localPosModel_Die;
        GetComponent<Collider>().enabled = false;
        myRigidbody.isKinematic = true;
        isDie = true;
        await Task.Delay(2000);
        parentModel.DOScale(new Vector3(0, 0, 0), 1.4f).SetEase(Ease.InBack);
    }
    protected virtual async void ZoomUpEffect()
    {
        await Task.Delay(350);
        Observer.Noti(constr.LEVELUP);
        CountInDeadZone = 0;
        scaleValue = ((float)level - 1) / paramForZoom + 1f;
        scaleSpeed = ((float)level - 1) / paramForScaleSpeed + 1f;
        transform.DOScale(scaleValue * new Vector3(1.15f,1.15f,1.15f), 0.2f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => {
                /// fixx
                transform.DOScale(scaleValue * new Vector3(0.9f, 0.9f, 0.9f), 0.15f).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    transform.DOScale(scaleValue * new Vector3(1f, 1f, 1f), 0.15f).SetEase(Ease.OutSine);
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
        if (!tmp&& currentCharacterAnimName== constr.TOIDLE) {
            StartCoroutine(setRun()); 
        }
    }
    IEnumerator setRun()
    {
        yield return new WaitForSeconds(0.21f);
        SetAnimation(AnimationType.RUN);
        currentCharacterAnimName = constr.IDLE;
        StopCoroutine(setRun());
    }
    public void resetMove()
    {
        if (isDie||isRun) return;
        SetAnimation(AnimationType.TOIDLE);
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
                ChangeAnim(constr.IDLE);
                break;
            case AnimationType.DIEMACE:
                ChangeAnim(constr.DIEMACE);
                break;
            case AnimationType.RUN:
                ChangeAnim(constr.RUN);
                break;
            case AnimationType.DIE:
                ChangeAnim(constr.DIE);
                break;
            case AnimationType.ANMUNG:
                ChangeAnim(constr.ANMUNG);
                break;
            case AnimationType.TOIDLE:
                ChangeAnim(constr.TOIDLE);
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