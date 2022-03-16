using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieStateType
{
    Idling, Attacking, Following, Dying
}

public class ZombieState : State
{
    protected ZombieComponent ownerZombie;

    protected readonly int movementZHash = Animator.StringToHash("MovementZ");
    protected readonly int isAttackingHash = Animator.StringToHash("IsAttacking");
    protected readonly int isDeadHash = Animator.StringToHash("IsDead");

    public ZombieState(ZombieComponent zombie, ZombieStateMachine stateMachine) : base (stateMachine)
    {
        ownerZombie = zombie;
    }

    public override void Update()
    {
        base.Update();
        float moveZ = ownerZombie.NavMeshAgent.velocity.magnitude / ownerZombie.NavMeshAgent.speed;
        ownerZombie.Animator.SetFloat(movementZHash, moveZ);
    }
}