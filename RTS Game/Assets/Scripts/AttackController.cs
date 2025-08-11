using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targettoAttack; // The target to attack

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && targettoAttack == null)        
        {
            targettoAttack = other.transform; // Set the target to the enemy that entered the trigger   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && targettoAttack !=null )        
        {
            targettoAttack = null; // Clear the target when the enemy exits the trigger
        }
    }
}
