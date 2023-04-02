using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour, IStateSwitcher, IKillable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] protected float _farAttackDistance;
    [SerializeField] protected float _closeAttackDistance;
    [SerializeField] private float _viewDistance;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _obstructionMask;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] protected Weapon _weapon;
    [SerializeField] protected AudioClip _deathSound;

    public static Action<AudioClip> OnEnemyDeath;

    private bool _isPlayerAlive;
    private bool _isAlive;
    private bool _isHeadShot;
    private bool _canSeePlayer;
    private float _health;
    protected GameObject _target;
    protected Animator _animator;
    private NavMeshAgent _navAgent;
    private BaseState _currentState;
    private List<BaseState> _states;
    private void OnEnable() => PlayerStats.OnPlayerDeath += Inactivate;
    private void OnDisable() => PlayerStats.OnPlayerDeath -= Inactivate;
    public void Start()
    {
        if (!_weapon) _farAttackDistance = _closeAttackDistance;
        _isPlayerAlive = true;
        _isAlive = true;
        _isHeadShot = false;
        _target = GameObject.FindGameObjectWithTag("AttackTarget");
        _health = _maxHealth;
        _animator= GetComponent<Animator>();
        _navAgent= GetComponent<NavMeshAgent>();
        _states = new List<BaseState>() 
        { 
            new WalkState(_animator, _navAgent),
            new ChaseState(_animator, _navAgent),
            new AttackState(_animator, _navAgent),
            new DeadState(_animator, _navAgent, _isHeadShot)
        };
        SwitchState<WalkState>();
    }
    public void Update()
    {
        if (_isPlayerAlive)
        {
            if (_isAlive)
            {
                if (_health <= 0 || _isHeadShot)
                {
                    OnEnemyDeath?.Invoke(_deathSound);
                       _isAlive = false;
                    SwitchState<DeadState>();
                    return;
                }
                _currentState?.Tick();             

                if (_canSeePlayer)
                {
                    if (CanAttackPlayer())
                    {
                        transform.LookAt(new Vector3(_target.transform.position.x, _target.transform.position.y-0.5f, _target.transform.position.z));
                        
                        _viewAngle = 360;
                        SwitchState<AttackState>();

                        return;
                    }
                    else SwitchState<ChaseState>();
                } 
                else CanSeePlayer();
            }
            else
                Destroy(gameObject, _timeToDestroy);
        }
        
    }
    public void SwitchState<T>() where T : BaseState
    {
        var newState = _states.FirstOrDefault(s => s is T);
        _currentState?.Exit();          
        _currentState = newState;       
        newState.Enter();               
    }
    private void CanSeePlayer() 
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, _viewDistance, _playerLayer);
        
        if (rangeCheck.Length !=0)
        {
            Transform target = rangeCheck[0].transform;
            
            Vector3 directionToTarget = (target.position - transform.position).normalized;            

            if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                {
                    _canSeePlayer = true;
                    return;
                }
            }            
        }       
       _canSeePlayer = false;
    }
    private bool CanAttackPlayer() 
    {
        if (Vector3.Distance(transform.position, _target.transform.position) <= _farAttackDistance)
            return true;
        return false;
    }

    public void HeadShot() => _isHeadShot = true;

    public void BodyShot(float damage)
    {
        _health -= damage;        
        if(_health >= 0) SwitchState<ChaseState>();
    }

    private void Inactivate()
    {
        _isPlayerAlive = false;
        SwitchState<WalkState>();
    }
}
