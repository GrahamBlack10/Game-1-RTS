using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
   Camera cam; // Reference to the main camera
   NavMeshAgent agent; // Reference to the NavMeshAgent component
    public LayerMask ground; // Layer mask to specify what is considered ground
    public bool isCommandedToMove; // Flag to check if the unit is commanded to move 

    Animator animator; // Reference to the Animator component (if needed for animations)

    private void Start()
        {
        cam = Camera.main; // Get the main camera
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject
        animator = GetComponent<Animator>();
    }
    private void Update()
        {
        // Check if the right mouse button is clicked
        if (Input.GetMouseButtonDown(1))
            {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera through the mouse position
            RaycastHit hit; // Variable to store information about what the ray hits
            // Perform the raycast and check if it hits something on the ground layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                isCommandedToMove = true; // Set the flag to true indicating the unit is commanded to move
                agent.SetDestination(hit.point); // Set the agent's destination to the point where the ray hit
                animator.SetBool("isMoving", true); // Set the walking animation state to true
            }
            }
        // Agent reached destination
        if (agent.hasPath == false ||  agent.remainingDistance <+ agent.stoppingDistance)
        {
            isCommandedToMove = false; // If the agent has no path or the remaining distance is less than or equal to the stopping distance, set the flag to false
            animator.SetBool("isMoving", false); // Set the walking animation state to false
        }
        else
        {
            animator.SetBool("isMoving", true); // Set the walking animation state to true
        }
    }

}
