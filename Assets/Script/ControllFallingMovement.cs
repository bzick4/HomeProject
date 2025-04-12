using UnityEngine;

public class ControllFallingMovement : StateMachineBehaviour
{
    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        animator.GetComponent<Controller>().IsHasPlayerContol = false;
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        animator.GetComponent<Controller>().IsHasPlayerContol = true;
    }
}
