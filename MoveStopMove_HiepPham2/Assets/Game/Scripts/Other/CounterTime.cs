using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime
{
    private UnityAction action;
    private float time;
    public bool IsRunning => time > 0.0f;

    public void Start(UnityAction action, float time)
    {
        this.action = action;
        this.time = time;
    }

    public void Execute()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Exit();
            }
        }
    }

    public void Exit()
    {
        action?.Invoke();
    }

    public void Cancel()
    {
        action = null;
        time = -1;
    }
}
