using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }
    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> selectedUnits = new List<GameObject>();
    public LayerMask clickable; // Layer mask to specify what is considered clickable
    public LayerMask ground; // Layer mask to specify what is considered ground
    public LayerMask attackable; // Layer mask to specify what is considered attackable
    public Camera cam; // Reference to the main camera
    public GameObject groundMarker;
    public bool attackCursorVisible;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // If we are hitting a clickable object
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }

            }
            else // if we are not hitting a clickable object
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // If we are hitting a clickable object
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
               groundMarker.transform.position = hit.point; // Move the ground marker to the clicked position
                groundMarker.SetActive(false);
                groundMarker.SetActive(true); // Show the ground marker
            }
        }

        //Attack Target 
        if (selectedUnits.Count > 0 && AtleastOneOffensiveUnit(selectedUnits))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // If we are hitting a clickable object
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                Debug.Log("Enemy Hovered with Mouse");

                attackCursorVisible = true; // Show the attack cursor

                if (Input.GetMouseButtonDown(1)) // Right click to attack
                {
                    Transform target = hit.transform; // Get the target to attack

                    foreach (GameObject unit in selectedUnits)
                    {
                        if (unit.GetComponent<AttackController>()) // Check if the unit has an AttackController component
                        {
                            unit.GetComponent<AttackController>().targettoAttack = target; // Set the target for the attack
                        }
                    }
                }
            }
            else
            {
                attackCursorVisible = false; // Hide the attack cursor
            }
        }
    }

    private bool AtleastOneOffensiveUnit(List<GameObject> selectedUnits)
    {
        foreach (GameObject unit in selectedUnits)
        {
            if (unit.GetComponent<AttackController>()) // Check if the unit has an AttackController component
            {
                return true; // If at least one unit has an AttackController, return true
            }  
        }
        return false; // If no units have an AttackController, return false
    }

    private void MultiSelect(GameObject unit)
    {
        if (selectedUnits.Contains(unit) == false)
        {
           selectedUnits.Add(unit); // Add the clicked unit to the selected units list
            SelectUnit(unit, true);
        }
        else
        {
            SelectUnit(unit, false);
            selectedUnits.Remove(unit); // Remove the unit from the selected units list
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in selectedUnits)
        {
            SelectUnit(unit, false);
        }
        groundMarker.SetActive(false);
        selectedUnits.Clear(); // Clear the selected units list
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll(); // Deselect all units first

        selectedUnits.Add(unit); // Add the clicked unit to the selected units list

        SelectUnit(unit, true);
    }

    private void SelectUnit(GameObject unit, bool isSelected)
    {
        TriggerSelectionIndicator(unit, isSelected);
        EnabledUnitMovement(unit, isSelected);
    }
    private void EnabledUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove; 
    }
    
    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.Find("Indicator").gameObject.SetActive(isVisible);
    }

    internal void DragSelect(GameObject unit)
    {
       if (!selectedUnits.Contains(unit))
        {
            selectedUnits.Add(unit); // Add the unit to the selected units list
            SelectUnit(unit, true);
        }
    }
}
