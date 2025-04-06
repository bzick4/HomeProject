using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _Speed = 5f;
    [SerializeField] private float  _RunSpeed = 9f;
    [SerializeField] private float  _JumpForce = 5f;
    [SerializeField] private float  _RotSpeed= 600f;
    [SerializeField] private MainCameraController _MMC;

    private Quaternion _requireRotation;
    private Animator _animator;

    [Header("Player collision & gravity")]
    [SerializeField] private float _SurfaceCheckRadius = 0.1f;
    [SerializeField] private float _FallingSpeed;
    [SerializeField] private Vector3 _MoveDir;

    public Vector3 SurfaceCheckOffset;
    public LayerMask SurfaceLayer;

    private CharacterController _characterController;
    private float _movementAmount;
    private float _currentSpeed;
    private bool _isRun;
    private bool _isOnSurface;
    



    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _isRun = Input.GetKey(KeyCode.LeftShift);

        Falling();
        PlayerMovement();
        Roll();
        SurfaceCheck();
    }

    private void PlayerMovement()
    {
        float _horiz = Input.GetAxis("Horizontal");
        float _vert = Input.GetAxis("Vertical");

        _currentSpeed = _isRun ? _RunSpeed : _Speed;

        _movementAmount = Mathf.Clamp01(Mathf.Abs(_horiz) + Mathf.Abs(_vert));

        var movementInput = (new Vector3(_horiz, 0, _vert)).normalized;

        var moveDirection = _MMC.FlatRotation * movementInput;

        _characterController.Move(moveDirection * _currentSpeed * Time.deltaTime);

        if(_movementAmount > 0)
        {
        _requireRotation = Quaternion.LookRotation(moveDirection);
        }

        moveDirection = _MoveDir;

        _animator.SetFloat("movementValue",_isRun ? 2f : _movementAmount, 0.2f, Time.deltaTime);
       
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _requireRotation, _RotSpeed * Time.deltaTime);
  
    }

    private void Roll()
    {
        if(Input.GetKeyDown(KeyCode.C) && _movementAmount > 0f)
        {
            _animator.SetTrigger("Roll");
        }

    }
    
    private void SurfaceCheck()
    {
        _isOnSurface = Physics.CheckSphere(transform.TransformPoint(SurfaceCheckOffset), _SurfaceCheckRadius, SurfaceLayer);
    }

    private void Falling()
    {
        if(_isOnSurface) _FallingSpeed = -0.5f;
        else _FallingSpeed += Physics.gravity.y * Time.deltaTime;
       
        var velocity = _MoveDir * _Speed;
        velocity.y = _FallingSpeed;

        //if(_FallingSpeed < -0.5f) _animator.SetTarget();

    }

   private void OnDrawGizmosSelected() 
   {
    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(transform.TransformPoint(SurfaceCheckOffset), _SurfaceCheckRadius);
   }
}