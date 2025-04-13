using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _Speed = 5f;
    [SerializeField] private float  _RunSpeed = 9f;
    [SerializeField] private float  _RotSpeed = 600f;
    [SerializeField] private Transform cameraTransform;
    private PerimeterChecker _perimeterChecker;
    private bool isPlayerControl = true;
    private Quaternion _requireRotation;
    private Animator _animator;

    [Header("Player collision & gravity")]
    [SerializeField] private float _SurfaceCheckRadius = 0.1f;
    [SerializeField] private float _FallingSpeed;
    [SerializeField] private Vector3 _MoveDir;
    [SerializeField] private Vector3 _RequiredMoveDir;
     
    
    private Vector3 _velocity;
    public Vector3 SurfaceCheckOffset;
    public LayerMask SurfaceLayer;

    private CharacterController _characterController;
    public float MovementAmount {get; set;}
    private float _currentSpeed;
    private bool _isRun;
    public bool _isOnSurface {get; set;}
    public bool isPlayerOnLedge {get; set;}
    public LedgeInfo LedgeInfo {get; set;}
    



    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _perimeterChecker  = GetComponent<PerimeterChecker>();
    }
    private void Update()
    {
        PlayerMovement();
        LedgeCheck();
        if(!isPlayerControl) return;

        _isRun = Input.GetKey(KeyCode.LeftShift);
        
        SurfaceCheck();
        _animator.SetBool("isOnSurface", _isOnSurface);
    }

    private void PlayerMovement()
    {
        float _horiz = Input.GetAxis("Horizontal");
        float _vert = Input.GetAxis("Vertical");

        _currentSpeed = _isRun ? _RunSpeed : _Speed;

        MovementAmount = Mathf.Clamp01(Mathf.Abs(_horiz) + Mathf.Abs(_vert));

        var movementInput = new Vector3(_horiz, 0, _vert).normalized;
        _RequiredMoveDir =  Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementInput;

        _characterController.Move(_velocity * Time.deltaTime);

        if(MovementAmount > 0 && _MoveDir.magnitude > 0.2f)
        {
           _requireRotation = Quaternion.LookRotation(_RequiredMoveDir);
           transform.rotation = _requireRotation;
        }
       
        _MoveDir = _RequiredMoveDir;

        _requireRotation = Quaternion.RotateTowards(transform.rotation, _requireRotation, _RotSpeed * Time.deltaTime);
    }

    
    private void SurfaceCheck()
    {
        _isOnSurface = Physics.CheckSphere(transform.TransformPoint(SurfaceCheckOffset), _SurfaceCheckRadius, SurfaceLayer);
    }

    private void PlayerLedgeMovement()
    {
        float angle  = Vector3.Angle(LedgeInfo.SurfaceHit.normal, _RequiredMoveDir);
        if(angle < 90)
        {
            _velocity = Vector3.zero;
            _MoveDir = Vector3.zero;
        }
    }

    
    private void LedgeCheck()
    {
        if(!isPlayerControl) return;

        _velocity = Vector3.zero;

        if(_isOnSurface)
        {
            _FallingSpeed = -0.5f;
            _velocity = _MoveDir * _currentSpeed;
            
            isPlayerOnLedge = _perimeterChecker.CheckLedge(_MoveDir, out LedgeInfo ledgeInfo);

            if(isPlayerOnLedge)
            {
                LedgeInfo = ledgeInfo;
                PlayerLedgeMovement();
                Debug.Log("player on ledge");
            }
            _animator.SetFloat("movementValue",_isRun ? 2f : _velocity.magnitude / _currentSpeed, 0.2f, Time.deltaTime);
        }
        else 
        {
            _FallingSpeed += Physics.gravity.y * Time.deltaTime;
            _velocity = transform.forward * _Speed / 2;
        }

        _velocity.y = _FallingSpeed;
    

    }

   private void OnDrawGizmosSelected() 
   {
    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(transform.TransformPoint(SurfaceCheckOffset), _SurfaceCheckRadius);
   }

   public void SetControl(bool isHasContol)
   {
    this.isPlayerControl = isHasContol;
    _characterController.enabled = isHasContol;

    if(!isHasContol)
    {
        _animator.SetFloat("movementValue",0f);
        _requireRotation = transform.rotation;
    }
   }



   public float RotSpeed => _RotSpeed;
   public float FallingSpeed => _FallingSpeed;
   public bool IsHasPlayerContol
   {
    get => isPlayerControl;
    set => isPlayerControl = value;
   }
   public Vector3 Velocity => _velocity;

}