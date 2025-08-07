using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
   Camera cam; // Reference to the main camera
   NavMeshAgent agent; // Reference to the NavMeshAgent component
    public LayerMask ground; // Layer mask to specify what is considered ground
    private void Start()
        {
        cam = Camera.main; // Get the main camera
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject
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
                agent.SetDestination(hit.point); // Set the agent's destination to the point where the ray hit
                }
            }
    }

}
