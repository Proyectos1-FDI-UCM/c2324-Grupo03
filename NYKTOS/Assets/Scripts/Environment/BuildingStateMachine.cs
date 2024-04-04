using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStateMachine : MonoBehaviour
{
    public enum BuildingState
    {
        NotBuilt,
        Built
    }

    private BuildingState _buildingState = BuildingState.NotBuilt;
    public BuildingState buildingState
    {
        get { return _buildingState; }
    }

    public void SetState(BuildingState state)
    {
        _buildingState = state;
    }

    private bool _isInteractable = true;

    public bool isInteractable { 
        get { return _isInteractable; } 
        set { _isInteractable = value; } 
    }
}
