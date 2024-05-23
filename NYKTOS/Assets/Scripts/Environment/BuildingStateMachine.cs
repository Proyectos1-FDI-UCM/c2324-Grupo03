using UnityEngine;

/// <summary>
/// Máquina de estados de los edificios
/// 2 estados posibles: construido y no construido
/// Además, determina si son interactuables o no
/// </summary>
public class BuildingStateMachine : MonoBehaviour
{
    public enum BuildingState
    {
        NotBuilt,
        Built
    }

    [SerializeField]
    private BuildingState _buildingState = BuildingState.NotBuilt;
    public BuildingState buildingState
    {
        get { return _buildingState; }
    }

    public void SetState(BuildingState state)
    {
        _buildingState = state;
    }

    [SerializeField]
    private bool _isInteractable = true;

    public bool isInteractable { 
        get { return _isInteractable; } 
        set { _isInteractable = value; } 
    }
}
