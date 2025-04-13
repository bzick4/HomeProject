using UnityEngine;

public class ControllFallingMovement : StateMachineBehaviour
{
    private void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        animator.GetComponent<Controller>().IsHasPlayerContol = false;
    }

    private void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        animator.GetComponent<Controller>().IsHasPlayerContol = true;
    }
}
