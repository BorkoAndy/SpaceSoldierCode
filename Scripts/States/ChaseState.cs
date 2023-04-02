using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    public ChaseState(Animator animator, NavMeshAgent navAgent)
    {
        _animator = animator;
        _navAgent = navAgent;
    }
    private GameObject _player;
    public override void Enter()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator.SetBool("isChasing",true);
    }
    public override void Tick()
    {
        if (_player)
            _navAgent.SetDestination(_player.transform.position);
    }

    public override void Exit() => _animator.SetBool("isChasing", false);

}
