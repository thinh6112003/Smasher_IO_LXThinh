using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : Istate
{
    void Istate.OnEnter(Enemy enemy)
    {
        Debug.Log("die");
        Observer.Noti(constr.ONEMOREKILL);
        enemy.currentEnemyState = CurrentEnemyState.DIE;
        DOTween.Kill(enemy.transform);
        enemy.myAgent.isStopped = true;
    }

    void Istate.OnExit(Enemy enemy)
    {
    }

    void Istate.OnUpdate(Enemy enemy)
    {
    }
}
