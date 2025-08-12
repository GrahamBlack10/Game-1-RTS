using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    AttackController attackController;
    public float stopAttackingDistance = 1.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        attackController = animator.GetComponent<AttackController>();
        attackController.SetAttackMaterial(); // Set the material to attack state

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackController.targettoAttack != null && animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
        {
           LookAtTarget();

            agent.SetDestination(attackController.targettoAttack.position);
            // Check distance to target
            float distanceFromTarget = Vector3.Distance(attackController.targettoAttack.position, animator.transform.position);
            // If close enough, stop moving and attack
            if (distanceFromTarget > stopAttackingDistance || attackController.targettoAttack == null)
            {
                animator.SetBool("isAttacking", false); // Move to Follow State
            }
        }


    }

    private void LookAtTarget()
    {
        Vector3 direction = attackController.targettoAttack.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0); // Keep only the Y rotation to avoid tilting

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
