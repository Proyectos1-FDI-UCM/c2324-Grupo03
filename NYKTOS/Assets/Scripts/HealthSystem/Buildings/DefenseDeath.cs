using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDeath : MonoBehaviour, IDeath
{
    #region references
    private DefenseComponent _defenseComponent;
    private BuildingStateMachine _placeholderState;
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
        //Arreglar el estado construido/destruido
        Destroy(gameObject);
        Debug.Log("Morido");
        _placeholderState.SetState(BuildingStateMachine.BuildingState.NotBuilt);
        Debug.Log(_placeholderState.buildingState);
    }
}
