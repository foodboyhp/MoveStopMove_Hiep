using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.SetDestination(LevelManager.Ins.RandomPoint());
    }

    public void OnExecute(Enemy t)
    {
        if (t.IsDestination)
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy t)
    {

    }

}
