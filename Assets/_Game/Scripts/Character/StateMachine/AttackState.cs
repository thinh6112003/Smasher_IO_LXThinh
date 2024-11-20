using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class AttackState : Istate
{
    Enemy myself;
    public void OnEnter(Enemy enemy)
    {
        enemy.myAgent.isStopped = true;
        enemy._rigidBody.velocity = Vector3.zero;
        enemy.myAgent.nextPosition = enemy.transform.position;
        enemy.currentEnemyState = CurrentEnemyState.ATTACK;
        myself = enemy;
        enemy.myAgent.isStopped = true;
        if (enemy.target != null)
            enemy.transform.DOLookAt(enemy.target.transform.position, 0.1f)
            .OnComplete(() =>
            {
                enemy.SetAttack();
            });
        else enemy.SetAttack();
        Observer.AddListener(constr.IDLE + enemy.gameObject.GetHashCode(),SetEndAttack);
    }
    public void OnExit(Enemy enemy)
    {
        enemy.target = null;
    }
    public void OnUpdate(Enemy enemy)
    {
    }
    public void SetEndAttack()
    {
        myself.ChangeState(new PatrolState());
    }
}
