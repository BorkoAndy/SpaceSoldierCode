using UnityEngine;
using UnityEngine.AI;

public class WalkState : BaseState
{
    public WalkState(Animator animator, NavMeshAgent navAgent)
    {
        _animator = animator;
        _navAgent = navAgent;
    }
    private Transform _target;
    public override void Enter()
    {
        PickTarget();
        
        _animator.SetBool("isWalking", true);
    }
    public override void Tick()
    {
        if(_target)
            _navAgent.SetDestination(_target.position);
        if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            PickTarget();
    }

    public override void Exit() => _animator.SetBool("isWalking", false);

    private void PickTarget()
    {
        var points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        _target = points[Random.Range(0, points.Length)].transform;
    }
}
