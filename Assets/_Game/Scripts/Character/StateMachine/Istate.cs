using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface Istate
{
    void OnEnter(Enemy enemy);
    void OnUpdate(Enemy enemy);
    void OnExit(Enemy enemy);
}
