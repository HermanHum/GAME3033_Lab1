using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieState
{
    GameObject followTarget;
    float attackRange = 2;

    public ZombieAttackState(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        followTarget = _followTarget;
        UpdateInterval = 2f;

        // Set damageable object here, ADD LATER
    }

    public override void Start()
    {
        base.Start();
        ownerZombie.NavMeshAgent.isStopped = true;
        ownerZombie.NavMeshAgent.ResetPath();
        ownerZombie.Animator.SetBool(isAttackingHash, true);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        // Deal damage every interval
    }

    public override void Update()
    {
        base.Update();
        Vector3 lookAtPosition = new Vector3(followTarget.transform.position.x, ownerZombie.transform.position.y, followTarget.transform.position.z);
        ownerZombie.transform.LookAt(lookAtPosition, Vector3.up);
        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBetween > attackRange)
        {
            // Change state to following
            stateMachine.ChangeState(ZombieStateType.Following);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.NavMeshAgent.isStopped = false;
        ownerZombie.Animator.SetBool(isAttackingHash, false);
    }
}
