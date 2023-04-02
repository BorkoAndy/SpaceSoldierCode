using UnityEngine;

public class Vanguard : EnemyBehaviour, IHasWeapon
{    
    private void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, _target.transform.position) <= _closeAttackDistance)
            _animator.SetBool("CloseAttack", true);
        else _animator.SetBool("CloseAttack", false);
    }
    public void Shoot() => _weapon.DoDamage();
}
