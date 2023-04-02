using UnityEngine;
using UnityEngine.AI;

public class DeadState : BaseState
{
    private bool _isHeadShot;
    
    public DeadState(Animator animator, NavMeshAgent navAgent, bool isHeadShot) 
    {
        _animator = animator;
        _navAgent = navAgent;
        _isHeadShot = isHeadShot;
    }
    public override void Enter()
    {
        _navAgent.isStopped= true;  
        if (_isHeadShot)
            _animator.SetTrigger("HeadShot");
        else
            _animator.SetTrigger("isDead");       
    }
    
    public override void Tick(){}
    public override void Exit() { }
}
