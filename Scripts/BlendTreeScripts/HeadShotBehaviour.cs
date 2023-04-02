using UnityEngine;

public class HeadShotBehaviour : StateMachineBehaviour
{
    [SerializeField] private int _headShotAnimationsCount;
    private int _headShotAnimation;    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _headShotAnimation = Random.Range(0, _headShotAnimationsCount);
        animator.SetFloat("Blend", _headShotAnimation);
    }    
}
