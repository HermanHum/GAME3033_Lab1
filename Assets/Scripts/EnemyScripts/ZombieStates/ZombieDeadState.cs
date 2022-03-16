using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : ZombieState
{
    public ZombieDeadState(ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
    }

    public override void Start()
    {
        base.Start();
        ownerZombie.NavMeshAgent.isStopped = true;
        ownerZombie.NavMeshAgent.ResetPath();
        ownerZombie.Animator.SetBool(isDeadHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.Animator.SetBool(isDeadHash, false);
    }
}
