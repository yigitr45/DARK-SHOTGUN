using UnityEngine;

public class Dash_Behaviour : StateMachineBehaviour
{
    //public Karakter Karakter { get; set; }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Karakter = FindObjectOfType<Karakter>();

        Karakter.KarakterKod.Dash = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Karakter.KarakterKod.Dash = false;

        if (Karakter.KarakterKod.lookingRight)
        {
            Karakter.KarakterKod.rotation.y = 0;
            Karakter.KarakterKod.recoilDirection.x = -1;
        }
        else
        {
            Karakter.KarakterKod.rotation.y = 180;
            Karakter.KarakterKod.recoilDirection.x = 1;
        }

        Karakter.KarakterKod.transform.eulerAngles = Karakter.KarakterKod.rotation;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
