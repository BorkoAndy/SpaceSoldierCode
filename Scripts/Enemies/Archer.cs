using UnityEngine;

public class Archer :EnemyBehaviour, IHasWeapon
{    
    [SerializeField] private Transform _arrowSpawnPoint;    
    [SerializeField] private float _arrowSpeed; 
    
    private void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, _target.transform.position) <= _closeAttackDistance)
            _animator.SetBool("CloseAttack", true);
        else _animator.SetBool("CloseAttack", false);        
    }
    public void Shoot()
    {
        var newArrow = Instantiate(_weapon, _arrowSpawnPoint.position, transform.rotation);        
        newArrow.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _arrowSpeed, ForceMode.Impulse);
    }    
}
