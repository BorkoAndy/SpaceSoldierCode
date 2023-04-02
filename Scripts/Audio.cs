using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _elevatorSound;
    [SerializeField] private AudioClip _playerDeathSound;
    private AudioSource _audioSource;
    private void OnEnable()
    {
        ShootAndHit.OnShotMade += PlayShotSound;
        Lift.OnElevatorMove += PlayerElevatorSound;
        Weapon.PlayAttackSound += PlayAttackSound;
        EnemyBehaviour.OnEnemyDeath += PlayEnemyDeathSound;
        PlayerStats.OnPlayerDeath += PlayPlayerDeathSound;
        
    }
    private void Start() => _audioSource = GetComponent<AudioSource>();

    private void PlayerElevatorSound() => _audioSource.PlayOneShot(_elevatorSound);

    private void PlayShotSound() => _audioSource.PlayOneShot(_shotSound);

    private void PlayAttackSound(AudioClip weaponSound) => _audioSource.PlayOneShot(weaponSound);
    private void PlayEnemyDeathSound(AudioClip deathSound) => _audioSource.PlayOneShot(deathSound);
    private void PlayPlayerDeathSound() => _audioSource.PlayOneShot(_playerDeathSound);    
}
