using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{    
    public AttackState(Animator animator, NavMeshAgent navAgent)
    {
        _animator = animator;
        _navAgent = navAgent;
    }
    public override void Enter()
    {
        _animator.SetTrigger("Attack");
        _navAgent.isStopped= true;        
    }
    public override void Tick() { }
    public override void Exit() => _navAgent.isStopped = false;

}
