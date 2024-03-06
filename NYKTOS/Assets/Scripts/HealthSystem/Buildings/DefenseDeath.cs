using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDeath : MonoBehaviour, IDeath
{
    #region references
    private DefenseComponent _defenseComponent;
    private BuildingStateMachine _placeholderState;
    [SerializeField] private BuildingManager _buildingManager;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _defenseComponent = GetComponent<DefenseComponent>();
        _placeholderState = _defenseComponent.placeholder.GetComponent<BuildingStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Death()
    {
        _buildingManager.RemoveBuilding(this.gameObject);
        Destroy(gameObject);
        _placeholderState.SetState(BuildingStateMachine.BuildingState.NotBuilt);
    }
}
