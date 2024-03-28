using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.OnMoveStop();
        t.Counter.Start(() => t.ChangeState(new PatrolState()), Random.Range(0f, 2f));
    }

    public void OnExecute(Enemy t)
    {
        t.Counter.Execute();
    }

    public void OnExit(Enemy t)
    {

    }

}
