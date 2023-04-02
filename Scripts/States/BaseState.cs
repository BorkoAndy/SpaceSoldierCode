using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState 
{
    protected Animator _animator;
    protected NavMeshAgent _navAgent;

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}
