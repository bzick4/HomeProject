using TMPro.EditorUtilities;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _Speed = 5f, _RunSpeed = 9f, _Acceleration = 10f, _JumpForce = 5f;
    private Rigidbody _rb;
    public Animator _anim {get; set;}

    private bool isGrounded, isMove;
    private float _currentSpeed;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _anim.applyRootMotion=true;

        _rb.freezeRotation = true;
        _currentSpeed = _Speed;
        isMove = true;
    }

    void Update()
    {
        Run();
        Roll();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) Jump();
    }

    private void  FixedUpdate() 
    {
        if(isMove) Move();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        Vector3 targetVelocity = moveDirection * _currentSpeed;
        _rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3(targetVelocity.x, _rb.velocity.y, targetVelocity.z), _Acceleration * Time.deltaTime);

        _anim.SetFloat("Velocity", _rb.velocity.magnitude);
         
    }

    private void Roll()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            _anim.SetTrigger("Roll");
        }
    }

    private void Jump()
    {
        isMove = false;
        _rb.velocity = new Vector3(_rb.velocity.x, _JumpForce, _rb.velocity.z);
        isGrounded = false;
    }

    private void Run()
    {
         if (Input.GetKeyDown(KeyCode.LeftShift) && isMove || Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded)
        {
            _currentSpeed = _RunSpeed;
            _anim.SetBool("isRun", true);
            Debug.Log($"{_currentSpeed}");
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _currentSpeed = _Speed;
            _anim.SetBool("isRun", false);
        }
    }



}