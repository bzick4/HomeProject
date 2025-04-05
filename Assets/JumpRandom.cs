using UnityEngine;

public class JumpRandom : StateMachineBehaviour
{

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
       animator.SetInteger("JumpID", Random.Range(0,3));
    }
}
