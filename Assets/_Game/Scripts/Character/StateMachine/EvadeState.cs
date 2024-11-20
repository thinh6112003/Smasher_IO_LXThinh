using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class EvadeState : Istate
{
    float timer = 0;
    public void OnEnter(Enemy enemy)
    {
        enemy.currentEnemyState = CurrentEnemyState.EVADE;

        Vector3 dir = enemy.MyEvadeVector* Random.Range(2f,4f);
        dir += enemy.transform.position;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;
        if (NavMesh.SamplePosition(dir, out hit, 1, 1))
        {
            finalPos = hit.position;
        }
        enemy.myAgent.SetDestination(finalPos);
    }
    public void OnUpdate(Enemy enemy)
    {
        if (enemy._rigidBody.velocity.sqrMagnitude < 0.01) {
            enemy.ChangeState(new PatrolState());
            return;
        }
        timer += Time.deltaTime;
        enemy._rigidBody.velocity = enemy.myAgent.velocity;
        if(enemy._rigidBody.velocity.sqrMagnitude > 0.0001f) enemy.SetRun(enemy._rigidBody.velocity);
        if (timer > 0.3f)
        {
            enemy.myAgent.nextPosition = enemy.transform.position;
            timer = 0;
        }
        if(Vector3.Distance(enemy.transform.position, enemy.myAgent.destination)<0.2f) enemy.ChangeState(new PatrolState());
    }
    public void OnExit(Enemy enemy)
    {

    }
    
}
