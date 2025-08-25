using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button buildButton;
    public PlacementSystem placement;

    private void Start()
    {
        if (buildButton == null)
        {
            Debug.LogError("BuildButton not assigned!");
            return;
        }

        // REMOVE any old listeners to prevent duplicates
        buildButton.onClick.RemoveAllListeners();

        // ADD your listener
        buildButton.onClick.AddListener(() => Construct(0));

        if (placement == null)
            Debug.LogError("PlacementSystem not assigned!");
    }


    private void Construct(int id)
    {
        Debug.Log("clicked");
        placement.StartPlacement(id);
    }
}
