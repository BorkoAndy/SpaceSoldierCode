using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   
    [SerializeField] private float _gravity;
    [SerializeField] private Camera _FPS_Camera;
    [SerializeField] private float _mouseSensitivity;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        _FPS_Camera.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    private void FixedUpdate()
    {    
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, _gravity, vertical);
        direction = transform.TransformDirection(direction);
        rigidBody.velocity = direction * moveSpeed;
    }

    private void Update()
    {
        MouseLook();
    }
    private void MouseLook()
    {
        float mouseVertical = Input.GetAxisRaw("Mouse Y");
        float mouseHorizontal = Input.GetAxisRaw("Mouse X");
        transform.Rotate(0, mouseHorizontal * _mouseSensitivity, 0);
       
        if((_FPS_Camera.transform.localRotation.x > -0.2f && mouseVertical > 0) ||
            (_FPS_Camera.transform.localRotation.x < 0.2f && mouseVertical < 0))
        {            
            _FPS_Camera.transform.Rotate(-mouseVertical * _mouseSensitivity, 0, 0);
        }
    }
}