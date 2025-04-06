using UnityEngine;

public class MainCameraController : MonoBehaviour
{
   [SerializeField] private Transform Target;
   [SerializeField] private float _RotX, _RotY, _RotSpeed;

   private float _gap = 3f;
   private  float _minVerAngle = -15f, _maxVerAngle = 45f;
   private float _invertXValue, _invertYValue;

   public Vector2 FramingBalance;
   public bool isInvertX, isInvertY;
   

   private void Start()
   {
    Cursor.lockState = CursorLockMode.Locked;
   }


   private void Update()
   {

    _invertXValue = (isInvertX) ? -1 : 1;
    _invertYValue = (isInvertY) ? -1 : 1;

    _RotX += Input.GetAxis("Mouse Y") * _invertYValue * _RotSpeed;
    _RotX = Mathf.Clamp(_RotX, _minVerAngle, _maxVerAngle);
    _RotY += Input.GetAxis("Mouse X") * _invertXValue * _RotSpeed;

    var targetRotation = Quaternion.Euler(_RotY, _RotY, 0);

    var focusPos = Target.position + new Vector3(FramingBalance.x, FramingBalance.y);

    transform.position = focusPos - targetRotation * new Vector3(0,0, _gap);
    transform.rotation = targetRotation;
   }

   public Quaternion FlatRotation =>  Quaternion.Euler(0, _RotY, 0);
   
}
