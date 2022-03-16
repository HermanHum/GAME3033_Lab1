using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    [SerializeField]
    private int zombieDamage = 5;
    public int ZombieDamage => zombieDamage;

    private NavMeshAgent navMeshAgent;
    public NavMeshAgent NavMeshAgent => navMeshAgent;
    private Animator animator;
    public Animator Animator => animator;
    private ZombieStateMachine zombieStateMachine;
    public ZombieStateMachine ZombieStateMachine => zombieStateMachine;
    [SerializeField]
    private GameObject followTarget;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        Initialize();
    }

    public void Initialize()
    {
        ZombieIdleState idleState = new ZombieIdleState(this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Idling, idleState);

        ZombieFollowState followState = new ZombieFollowState(followTarget, this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Following, followState);

        ZombieAttackState attackState = new ZombieAttackState(followTarget, this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Attacking, attackState);

        ZombieDeadState deadState = new ZombieDeadState(this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Dying, deadState);

        ZombieStateMachine.Initialize(ZombieStateType.Following);
    }
}
