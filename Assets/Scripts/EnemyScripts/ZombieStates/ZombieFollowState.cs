using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollowState : ZombieState
{
    GameObject followTarget;
    float stoppingDistance = 1;

    public ZombieFollowState(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        followTarget = _followTarget;
        UpdateInterval = 2f;
    }

    public override void Start()
    {
        base.Start();
        ownerZombie.NavMeshAgent.isStopped = false;
        ownerZombie.NavMeshAgent.ResetPath();
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        ownerZombie.NavMeshAgent.SetDestination(followTarget.transform.position);
    }

    public override void Update()
    {
        base.Update();

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBetween < stoppingDistance)
        {
            stateMachine.ChangeState(ZombieStateType.Attacking);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.NavMeshAgent.isStopped = true;
    }
}
