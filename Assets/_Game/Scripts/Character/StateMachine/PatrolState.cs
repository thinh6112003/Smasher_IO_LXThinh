using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : Istate
{
    int layerCharacter = 1 << 8;
    float updateTimer = 0;
    float updateInterval = 5f;
    float alertTimer = 0f;
    float alertInterval = 0.3f;
    float radius = 20;
    Vector3 nextPosition;
    Transform enemyTransform;
    Transform myTransform;
    Rigidbody rigidbody;
    NavMeshAgent myAgent;
    Enemy mySelf;
    Character enemy;

    public void OnEnter(Enemy enemy)
    {
        enemy.currentEnemyState = CurrentEnemyState.PATROL;
        enemy.myAgent.isStopped = false;
        mySelf = enemy;
        myAgent = enemy.myAgent;
        myAgent.destination = enemy.transform.position;
        myTransform = enemy.transform;
        nextPosition = myTransform.position;
        updateInterval = Random.Range(2, 6);
        rigidbody = mySelf._rigidBody;
        myAgent.nextPosition = myTransform.position;
    }
    public void OnUpdate(Enemy enemy)
    {
        Debug.Log("patrol ");
        enemy.SetAnimation(AnimationType.RUN);
        MoveRandom();
        FindEnemy();
    }
    public void OnExit(Enemy enemy)
    {
    }
    public void MoveRandom()
    {
        if (Vector3.Distance(nextPosition, myTransform.position) <= 1.5f)
        {
            nextPosition = RandomPoint(myTransform.position, radius);
            myAgent.SetDestination(nextPosition);
        }
        else
        {
            rigidbody.velocity = myAgent.velocity;
            if (rigidbody.velocity.sqrMagnitude > 0.0001f)
            {
                mySelf.SetRun(rigidbody.velocity);
            }
        }
        Alert();
    }
    public void Alert()
    {
        alertTimer += Time.deltaTime;
        if (alertTimer >= alertInterval)
        {
            if (CheckToEvade()) return;
            if (CheckToChangePursue()) return;
            alertTimer = 0;
        }
    }
    public bool CheckToEvade()
    {
        mySelf.collider.enabled = false;
        Collider[] hit = Physics.OverlapSphere(myTransform.position, 3f, layerCharacter);
        mySelf.collider.enabled = true;

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
        mySelf.collider.enabled = false;
        Collider[] hit = Physics.OverlapSphere(myTransform.position, 4.2f, layerCharacter);
        mySelf.collider.enabled = true;

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
    public void FindEnemy()
    {
        myAgent.nextPosition = myTransform.position;
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            if (GetEnemy())
            {
                mySelf.target = enemy;
                mySelf.ChangeState(new PursueState());
            }
            updateTimer = 0;
        }
    }
    public Vector3 RandomPoint(Vector3 startPoint, float radius)
    {
        Vector3 dir = Random.insideUnitSphere * radius;
        dir += startPoint;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;
        if (NavMesh.SamplePosition(dir, out hit, radius, 1))
        {
            finalPos = hit.position;
        }
        return finalPos;
    }
    public bool GetEnemy()
    {
        float radiusObserve = Random.Range(6f, 9f);
        mySelf.collider.enabled = false;
        Collider[] hit = Physics.OverlapSphere(myTransform.position, radius, layerCharacter);
        mySelf.collider.enabled = true;
        if (hit.Length == 0)
        {
            enemy = null;
            enemyTransform = null;
            return false;
        }
        float idSelect = Random.Range(0, hit.Length);
        enemy = hit[(int)idSelect].GetComponent<Character>();
        enemyTransform = hit[(int)idSelect].transform;
        return true;
    }
}