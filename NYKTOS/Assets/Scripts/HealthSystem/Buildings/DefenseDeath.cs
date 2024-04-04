using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDeath : MonoBehaviour, IDeath
{
    #region references
    private DefenseComponent _defenseComponent;
    private BuildingStateMachine _placeholderState;
    #endregion


    void Start()
    {
        _defenseComponent = GetComponent<DefenseComponent>();
        _placeholderState = _defenseComponent.placeholder.GetComponent<BuildingStateMachine>();
    }


    public void Death()
    {
        BuildingManager.Instance.RemoveBuilding(this.gameObject);
        _placeholderState.SetState(BuildingStateMachine.BuildingState.NotBuilt);
        _placeholderState.isInteractable = true;
        Destroy(gameObject);
    }
}
