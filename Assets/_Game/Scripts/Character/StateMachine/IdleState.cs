using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : Istate
{
    public void OnEnter(Enemy enemy)
    {
        enemy.SetAnimation(AnimationType.IDLE);
    }

    public void OnExit(Enemy enemy)
    {
    }

    public void OnUpdate(Enemy enemy)
    {
    }
}