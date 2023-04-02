using System;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private Animator _animator;
    private bool _isDown = true;
    public static Action OnElevatorMove;

    private void Start() => _animator = GetComponent<Animator>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_isDown) GoUp();
            else GoDown();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!_isDown) GoDown();
    }
    private void GoUp()
    {
        _animator.SetBool("isGoingUp", true);
        _isDown= false;
        OnElevatorMove?.Invoke();
    }
    private void GoDown()
    {
        _animator.SetBool("isGoingUp", false);
        _isDown = true;
        OnElevatorMove?.Invoke();
    }    
}
