using Cinemachine;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Player : Character
{
    #region properties
    [Header("Player properti")]
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private GameObject dustrail;
    [SerializeField] private GameObject LookDir;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private GameObject siuuuu;
    
    private bool isPlay = false;
    public static Character _intance;
    #endregion
    //[SerializeField] private GameObject mySkin;
    private void Awake()
    {

        Observer.AddListener(constr.ATTACK + gameObject.GetHashCode(), ShakeCamera);
        Observer.AddListener(constr.DONELOADLEVEL, Init);
        Observer.AddListener(constr.WINGAME, AnMung);
    }
    protected override void Start()
    {
        isPlay = true;
        _intance = this;
        base.Start();
        Init();
        Observer.Noti(constr.CHANGEWEAPON);
        Observer.Noti(constr.CHANGESKIN);
    }
    private void FixedUpdate()
    {
        if (!isDie&& isPlay) OnControlPlayer();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void Init()
    {
        Observer.AddListener(constr.DONECHANGECAM, ()=>SetControlPlayer(true));
        Observer.AddListener(constr.WINGAME, () => SetControlPlayer(false));
        base.Init();
        LookDir.SetActive(true);
        transform.position = Vector3.zero;
    }
    void SetControlPlayer(bool status)
    {
        isPlay = status;
        floatingJoystick.gameObject.SetActive(status);
    }
    protected override void Die(Weapon weaponhit, Transform enemyTf)
    {
        isPlay = false;
        base.Die(weaponhit, enemyTf);
        dustrail.SetActive(false);
        LookDir.SetActive(false);
        NotiLoseGame();
    }
    protected async void NotiLoseGame()
    {
        await Task.Delay(3000);
        Observer.Noti(constr.LOSEGAME);
    }
    protected override void Run(Vector3 moveVector)
    {
        base.Run(moveVector);
        dustrail.SetActive(true);
        myRigidbody.MovePosition(transform.position + moveVector);
    }
    protected override void Attack()
    {
        dustrail.SetActive(false);
        base.Attack();
    }
    protected void OnControlPlayer()
    {
        Vector3 moveVector = new Vector3(floatingJoystick.Horizontal, 0f, floatingJoystick.Vertical);

        if (moveVector.sqrMagnitude > 0.0001f) 
        {

            animCharacter.SetFloat(constr.MOVEBASE, 1);
            Run(moveVector.normalized*moveSpeed* scaleSpeed * Time.fixedDeltaTime);
        }
        else
        {
            currentCharacterAnimName = constr.IDLE;
            animCharacter.SetFloat(constr.MOVEBASE, 0);
            Attack();
        }
    }
    protected override void ZoomUpEffect()
    {
        base.ZoomUpEffect();
        Debug.Log("ZoomUpEffect"); 
        siuuuu.SetActive(true);
    }
    private void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
    private async void AnMung()
    {
        Hammer.gameObject.SetActive(false);
        LookDir.SetActive(false);
        transform.DOLookAt(transform.position + new Vector3(0, 0, -1), 0.5f).SetUpdate(UpdateType.Fixed);
        await Task.Delay(900);
        SetAnimation(AnimationType.ANMUNG);
        transform.DOScale(Vector3.one, 1.5f)
            .SetEase(Ease.Linear)
            .SetUpdate(UpdateType.Fixed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
public static class constr
{
    public static readonly string WIN = "Win";
    public static readonly string PLAYERPREFKEY = "Playerpref Key";
    public static readonly string CHARACTER = "Character";
    public static readonly string WEAPON = "Weapon";
    public static readonly string TOIDLE = "ToIdle";
    public static readonly string IDLE = "Idle";
    public static readonly string RUN = "Run";
    public static readonly string RUN_IDLE = "Run_idle";
    public static readonly string DIE = "Die";
    public static readonly string DIEMACE = "DieMace";
    public static readonly string ATTACKHAMMER = "AttackHammer";
    public static readonly string MOVEBASE = "MoveBase";
    public static readonly string ATTACK = "Attack";
    public static readonly string ATTACKMACE = "AttackMace";
    public static readonly string ATTACKAXE = "AttackAxe";
    public static readonly string BROCKEN = "Brocken";
    public static readonly string DEADZONE = "Deadzone";
    public static readonly string ANMUNG = "Anmung";
    public static readonly string PLAYER = "Player";
    public static readonly string CHANGE_CAMERA_PLAY = "Change Camera Play";
    public static readonly string CHANGE_CAMERA_LOBY_END = "Change Camera Loby End";
    public static readonly string DONECHANGECAM = "Done Change Cam";
    public static readonly string LEVELUP = "Level Up";
    public static readonly string DECOR = "Decor";
    public static readonly string CHANGESOUNDSTATUS = "ChangeSoundStatus";
    public static readonly string CHANGEVIBRATIONSTATUS = "ChangeVibrationStatus";
    public static readonly string UPDATEUI= "UpdateUI";
    public static readonly string DONELOADLEVEL= "done_load_level";
    public static readonly string RELOADLEVEL = "ReloadLevel";
    public static readonly string LOADSCENE= "Load";
    public static readonly string HOMESCENE= "Home";
    public static readonly string DONELOADSCENEASYNC= "DoneLoadSceneAsync";
    public static readonly string CHANGECAMFOLLOWRUN= "ChangeCamFollowRun";

    public static readonly string WINGAME = "WinGame";
    public static readonly string NEXTLEVEL= "NextLevel";
    public static readonly string ONEMOREKILL= "OneMoreKill";
    public static readonly string LOSEGAME= "LoseGame";
    public static readonly string CHANGESKIN = "Change Skin";
    public static readonly string CHANGEWEAPON = "Change Weapon";
    public static readonly string GETSHOPDATA = "Get Shop Data";
    public static readonly string SETSHOPDATA = "Set Shop Data";
}
