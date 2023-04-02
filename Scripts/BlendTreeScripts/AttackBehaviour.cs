using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    [SerializeField] private int _attackAnimationsCount;
    private int _attackAnimation = 0;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attackAnimation = Random.Range(0, _attackAnimationsCount+1);
        animator.SetFloat("Blend", _attackAnimation);
    }    
}
