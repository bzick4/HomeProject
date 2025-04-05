
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharContr : MonoBehaviour
{

    public float walkSpeed = 5f;    // Обычная скорость
    public float runSpeed = 8f;     // Скорость бега
    public float rotationSpeed = 10f; // Скорость поворота

    
    public float jumpHeight = 2f;   // Высота прыжка
    public float gravity = -20f;    // Гравитация

    private CharacterController _controller;
    private Vector3 _moveDirection;
    private float _verticalVelocity;
    private bool _isGrounded;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log($"{CurrentSpeed}");
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -0.5f; // Прижимаем к земле
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

       
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        _moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Применяем гравитацию
        _verticalVelocity += gravity * Time.deltaTime;

       
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        
        Vector3 move = _moveDirection * currentSpeed + Vector3.up * _verticalVelocity;
        _controller.Move(move * Time.deltaTime);

        
        if (_moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    
    public bool IsGrounded => _isGrounded;
    public Vector3 MoveDirection => _moveDirection;
    public float CurrentSpeed => _moveDirection.magnitude * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
    public float VerticalVelocity => _verticalVelocity;


   
    
 
}
