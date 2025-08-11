using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;

    NavMeshAgent agent;
    public float attackingDistance = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>();
        agent = animator.transform.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Should Unit Transition to Idle State
        if (attackController.targettoAttack == null)
        {
            animator.SetBool("isFollowing", false);
        }
        else
        {
            // if there is no other direct command to move the unit
            if (animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
            {
                // Moving Unit to Enemy
                agent.SetDestination(attackController.targettoAttack.position);
                animator.transform.LookAt(attackController.targettoAttack);

                // Should Unit Transition to Attack State
                //float distanceFromTarget = Vector3.Distance(attackController.targettoAttack.position, animator.transform.position);
                //if (distanceFromTarget < attackingDistance)   
                //{
                //    agent.SetDestination(animator.transform.position); // Stop moving when exiting the state
                //    animator.SetBool("isAttacking", true); // Move to Attack State
                //}
            }
        }
    }
}
