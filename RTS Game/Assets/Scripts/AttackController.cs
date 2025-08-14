using UnityEngine;
using UnityEngine.AI;

public class AttackController : MonoBehaviour
{
    public Transform targettoAttack; // The target to attack

    public Material idleStateMaterial; // Material for idle state
    public Material attackStateMaterial; // Material for attack state
    public Material followStateMaterial; // Material for follow state
    public int unitDamage;

    public bool isPlayer; // Flag to check if the unit is a player  

    public void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targettoAttack == null)        
        {
            targettoAttack = other.transform; // Set the target to the enemy that entered the trigger   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targettoAttack !=null )        
        {
            targettoAttack = null; // Clear the target when the enemy exits the trigger
        }
    }

    public void SetIdleMaterial()
    {
        GetComponent<Renderer>().material = idleStateMaterial; // Set the material to idle state
    }
    public void SetAttackMaterial()
    {
        GetComponent<Renderer>().material = attackStateMaterial; // Set the material to attack state
    }
    public void SetFollowMaterial()
    {
        GetComponent<Renderer>().material = followStateMaterial; // Set the material to follow state
    }
    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f*0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1.2f);



    }
}
