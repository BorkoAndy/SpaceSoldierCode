using UnityEngine;

public class DoorWithoutKey : SlidingDoor
{
    private Animator _animator;
    private float _positionDelta;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;  
    [SerializeField] private GameObject _doorModel;
    [SerializeField] private float _speed;
    

    
    private void Start()
    {
        _startPosition= _doorModel.transform.position;
       _animator= _doorModel.GetComponent<Animator>();
        _positionDelta = -5;
        _targetPosition = _doorModel.transform.position + transform.right * _positionDelta;
    }
    protected override void CloseDoor()
    {
        _animator.SetBool("isClosing", true);
        _animator.SetBool("isOpening", false);
        
        _doorModel.transform.position = Vector3.MoveTowards(_doorModel.transform.position, _startPosition, _speed * Time.deltaTime);
    }

    protected override void OpenDoor()
    {
        _animator.SetBool("isClosing", false);
        _animator.SetBool("isOpening", true);
        
        _doorModel.transform.position = Vector3.MoveTowards(_doorModel.transform.position, _targetPosition, _speed * Time.deltaTime);
    }    
}
