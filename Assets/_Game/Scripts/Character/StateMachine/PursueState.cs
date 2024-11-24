using UnityEngine;
using UnityEngine.AI;

public class PursueState : Istate
{
    int layerCharacter = 1 << 8;
    float alertTimer = 0f;
    float alertInterval = 0.3f;
    float updateTimer = 0;
    float updateInterval = 0.3f;
    float timePursue = 4;
    float timer = 0;
    Character myEnemy;
    Transform enemyTransform;
    Transform myTransform;
    NavMeshAgent myAgent;
    Enemy mySelf;

    public void OnEnter(Enemy enemy)
    {
        myEnemy = enemy.target;
        if (myEnemy.isDie) { 
            //enemy.ChangeState(new PatrolState());
            //return;
        }
        enemy.myAgent.isStopped = false;
        enemy.currentEnemyState = CurrentEnemyState.PURSUE;
        timer += Time.deltaTime;
        mySelf = enemy;
        myAgent = enemy.myAgent;
        myTransform = enemy.transform;
        enemyTransform = enemy.target.transform;
    }
    public void OnUpdate(Enemy enemy)
    {
        Move();
        UpdateDestination();
        Alert();
    }
    public void OnExit(Enemy enemy)
    {
        myEnemy = null;
    }
    public void UpdateDestination()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            myAgent.nextPosition = myTransform.position;
            if (myEnemy.isDie == true || Vector3.Distance(myTransform.position, enemyTransform.position) > 10f
            )
            {
                myEnemy = null;
                mySelf.ChangeState(new PatrolState());
                return;
            }
            if (timer > timePursue)
            {
                if (RandomBoolean.GetRandomGiveUp())
                {
                    mySelf.ChangeState(new PatrolState());
                    return;
                }
                else timer = 0;
            }
            if (Vector3.Distance(myTransform.position, enemyTransform.position) <= 2f * mySelf.scaleValue)
            {
                mySelf.ChangeState(new AttackState());
            }
            myAgent.destination = enemyTransform.position;
            updateTimer = 0f;
        }
    }

    public void Alert()
    {
        alertTimer += Time.deltaTime;
        if (alertTimer >= alertInterval)
        {
            if (CheckToEvade()) return;
            //if (CheckToChangePursue()) return;
            alertTimer = 0;
        }
    }
    public bool CheckToEvade()
    {
        mySelf.myCollider.enabled = false;
        Collider[] hit = Physics.OverlapSphere(myTransform.position, 3f*mySelf.scaleValue, layerCharacter);
        mySelf.myCollider.enabled = true;

        if (hit.Length > 0)
        {
            Vector3 evadeVector = Vector3.zero;
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[0].GetComponent<Character>().target == mySelf) evadeVector += (mySelf.transform.position - hit[0].transform.position).normalized;
            }
            mySelf.MyEvadeVector = evadeVector.normalized;
            if (RandomBoolean.GetRandomEvade())
            {
                mySelf.ChangeState(new EvadeState());
                return true;
            }
        }
        return false;
    }

    public bool CheckToChangePursue()
    {
        mySelf.myCollider.enabled = false;
        Collider[] hit = Physics.OverlapSphere(myTransform.position, 4.2f*mySelf.scaleValue, layerCharacter);
        mySelf.myCollider.enabled = true;

        if (hit.Length > 0)
        {
            Vector3 evadeVector = Vector3.zero;
            if (RandomBoolean.GetRandomEvade())
            {
                int index = Random.Range(0, hit.Length);
                mySelf.target = hit[index].GetComponent<Character>();
                mySelf.ChangeState(new PursueState());
                return true;
            }
        }
        return false;
    }

    public void Move()
    {
        mySelf._rigidBody.velocity = myAgent.velocity;
        if (mySelf._rigidBody.velocity.sqrMagnitude > 0.0001f)
        {
            mySelf.SetRun(mySelf._rigidBody.velocity);
        }
    }
}
