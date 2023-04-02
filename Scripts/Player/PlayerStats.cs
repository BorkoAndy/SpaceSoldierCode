using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _playerDead;
    [SerializeField] private Animator _cameraAnimator;
    

    public static Action OnPlayerDeath;

    private float _health;
    private void OnEnable() => Weapon.OnEnemyAttack += ReceiveDamage;
    private void OnDisable() => Weapon.OnEnemyAttack -= ReceiveDamage;
    private void Start()
    {
        _cameraAnimator.enabled = false;
        _health = _maxHealth;
    }

    private void ReceiveDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)        
            PlayerDies();      
    }
    private void PlayerDies()
    {
        OnPlayerDeath?.Invoke();
        _cameraAnimator.enabled = true;
        _playerDead.SetActive(true);
    }
}
