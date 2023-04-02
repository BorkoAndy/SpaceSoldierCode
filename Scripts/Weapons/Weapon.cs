using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected AudioClip _weaponSound;
    private GameObject _target;
    public static Action<float> OnEnemyAttack;
    public static Action<AudioClip> PlayAttackSound;

    private void Start() => _target = GameObject.FindGameObjectWithTag("Player");
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnEnemyAttack?.Invoke(_damage);
            PlayAttackSound?.Invoke(_weaponSound);            
        }
    }
   public void DoDamage()
    {
        PlayAttackSound?.Invoke(_weaponSound);
        Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToTarget, out hit))       
            if (hit.collider.CompareTag("Player"))                
                OnEnemyAttack?.Invoke(_damage);
    }
}
