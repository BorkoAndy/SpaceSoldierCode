using UnityEngine;

public class BodyShotBehaviour : StateMachineBehaviour
{
    [SerializeField] private int _bodyShotAnimationsCount;
    private int _bodyShotAnimation = 0;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bodyShotAnimation = Random.Range(0, _bodyShotAnimationsCount);
        animator.SetFloat("Blend", _bodyShotAnimation);
    }   
}
