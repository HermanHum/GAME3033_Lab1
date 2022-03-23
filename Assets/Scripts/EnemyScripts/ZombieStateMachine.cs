using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine : MonoBehaviour
{
    private State currentState;
    protected Dictionary<ZombieStateType, State> states = new Dictionary<ZombieStateType, State>();
    private bool isRunning;

    public void Initialize(ZombieStateType startingState)
    {
        if (states.ContainsKey(startingState))
        {
            ChangeState(startingState);
        }
    }

    public void AddState(ZombieStateType zombieStateType, State state)
    {
        if (states.ContainsKey(zombieStateType)) return;
        states.Add(zombieStateType, state);
    }

    public void RemoveState(ZombieStateType zombieStateType)
    {
        if (!states.ContainsKey(zombieStateType)) return;
        states.Remove(zombieStateType);
    }

    public void ChangeState(ZombieStateType nextState)
    {
        if (!states.ContainsKey(nextState)) return;
        if (isRunning)
        {
            StopRunningState();
        }

        currentState = states[nextState];
        currentState.Start();

        if (currentState.UpdateInterval >= 0)
        {
            InvokeRepeating(nameof(IntervalUpdate), 1f, currentState.UpdateInterval);
        }
        isRunning = true;
    }

    public void StopRunningState()
    {
        isRunning = false;
        currentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
    }

    private void IntervalUpdate()
    {
        if (isRunning)
        {
            currentState.IntervalUpdate();
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            currentState.Update();
        }
    }
}
