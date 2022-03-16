using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected ZombieStateMachine stateMachine;

    private float updateInterval = 0f;
    public float UpdateInterval { get { return updateInterval; } protected set { updateInterval = value; } }

    public State(ZombieStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void Start()
    {

    }

    public virtual void IntervalUpdate()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }
}
