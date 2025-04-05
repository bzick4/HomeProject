using UnityEngine;

public class Anim : MonoBehaviour
{
   [Header("Ссылки")]
    private CharContr movement;

  
    [SerializeField] private string speedParam = "Velocity", run = "isRun", groundedParam = "IsGrounded", jumpParam = "StandJump", _Roll = "Roll";

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        if (movement == null)
        {
            movement = GetComponentInParent<CharContr>();
        }
    }

    void Update()
    {
        AnimMove();
        AnimJump();
        Roll();
        // _animator.SetBool(groundedParam, movement.IsGrounded);
    }

    private void AnimMove()
    {
        float speedPercent = Mathf.Clamp01(movement.CurrentSpeed / movement.runSpeed);

        if(speedPercent > 0 && speedPercent <= 0.625)
        _animator.SetFloat(speedParam, speedPercent);
        
        if(speedPercent > 0.626)
        _animator.SetBool(run,true);

        if(movement.CurrentSpeed  == 0f)
        _animator.SetTrigger("Idle");



    }

    private void AnimJump()
    {
         if (movement.VerticalVelocity > 0 && !movement.IsGrounded)
        {
            _animator.SetTrigger(jumpParam);
        }
    }

     private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("JumpRandom")) 
    {
    int _randomJump = Random.Range(0, 6);
    _animator.SetInteger("JumpID", _randomJump);
    _animator.SetTrigger("JumpRand");
    //MoveOff();
    }
     //Invoke("MoveOn()", 3f);
}

 private void Roll()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            //MoveOff();
            _animator.SetTrigger(_Roll);
        }
        //Invoke("MoveOn()", 2f);
}

private void MoveOn()
{
    movement.enabled = true;
}
private void MoveOff()
{
    movement.enabled = false;
}


}
