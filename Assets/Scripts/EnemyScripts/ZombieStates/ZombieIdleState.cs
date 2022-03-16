using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieState
{
    public ZombieIdleState(ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {

    }

    public override void Start()
    {
        base.Start();
        ownerZombie.NavMeshAgent.isStopped = true;
        ownerZombie.NavMeshAgent.ResetPath();
    }
}
