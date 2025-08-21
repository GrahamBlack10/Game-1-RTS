using System;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private float unitHealth;
    public float unitMaxHealth;

    public HealthTracker healthTracker;

    Animator animator; // Reference to the Animator component
    NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      UnitSelectionManager.Instance.allUnitsList.Add(gameObject);

        unitHealth = unitMaxHealth;
        UpdateHealthUI();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    

    private void OnDestroy()
    {
            UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);   
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            // Dying Logic

            // Destruction or Dying animation

            // Dying Sound Effect


            Destroy(gameObject);
        }
    }
    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            
            animator.SetBool("isMoving", true); // Set the walking animation state to false
        }
        else
        {
            animator.SetBool("isMoving", false); // Set the walking animation state to true
        }
    }
}
