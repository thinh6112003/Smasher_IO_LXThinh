using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    // get set rigitbody 
    public Vector3 nextPosition;
    public Vector3 MyEvadeVector = new Vector3();
    public Rigidbody _rigidBody { get => myRigidbody; set => myRigidbody = value; }
    public CurrentEnemyState currentEnemyState;
    [SerializeField] public NavMeshAgent myAgent;
    private Istate currentState;
    private Vector3 originPos;
    [SerializeField]private bool test = false;

    private void Awake()
    {
        if (test) Init();
        originPos = transform.position;
    }

    protected override void Start()
    {
        base.Start();
    }
    protected void FixedUpdate()
    {
        if (currentState != null&& isDie == false)
        {
            currentState.OnUpdate(this);
        }
    }
    public void nDisable()
    {
        
    }
    protected override void Init()
    {
        base.Init();
        myAgent.enabled = false;
        myAgent.enabled = true;
        transform.position = originPos;
        ChangeState(new IdleState());
        Observer.AddListener(constr.DONECHANGECAM, () =>
        {
            ChangeState(new PatrolState());
        });
        myAgent.updatePosition = false;
        myAgent.speed = moveSpeed;
    }
    protected override void Die(Weapon weapon, Transform enemyTf)
    {
        base.Die(weapon,enemyTf);
        ChangeState(new DieState());
    }
    protected override void ZoomUpEffect()
    {
        base.ZoomUpEffect();
        myAgent.speed = moveSpeed * scaleSpeed;
    }
    public void SetRun(Vector3 moveVector)
    {
        Run(moveVector);
    }
    public void SetAttack()
    {
        Attack();
    }
    public void SetInit()
    {
        Init();
    }
    public void ChangeState(Istate newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(constr.DECOR))
        {
            ChangeState(new AttackState());
        }
    }
    public void Setspeed(float speed)
    {
        moveSpeed = speed;
        myAgent.speed = 0.01f;
    }
    public void SetSkin()
    {
        skinController.ChangeSkin();
    }
    public void SetWeapon()
    {
        //weaponController.ChangeWeapon();
    }
}
public enum CurrentEnemyState { PATROL, ATTACK, PURSUE,DIE, EVADE }

public class RandomBoolean
{
    public static bool GetRandom(int phantram)
    {
        return Random.Range(0, 100) < phantram;
    }
    public static bool GetRandomGiveUp()
    {
        return GetRandom(65);
    }
    public static bool GetRandomEvade()
    {
        return GetRandom(13);
    }
    public static bool GetRandomAttack()
    {
        return GetRandom(16);
    }
}

