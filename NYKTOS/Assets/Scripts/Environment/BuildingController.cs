using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour, IInteractable
{
    #region references
    private BuildingStateMachine _buildingState;
    
    #endregion
    public void Interact()
    {
        if (TryGetComponent(out IBuilding building) && GameManager.Instance.State != GameState.Night) building.OpenMenu();
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildingState = GetComponent<BuildingStateMachine>();
    }
}
