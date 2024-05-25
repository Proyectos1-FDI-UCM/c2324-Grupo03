using UnityEngine;

/// <summary>
/// Al destruir una defensa, ésta se quita de la lista del Building Manager 
/// y se actualiza el estado de construcción del placeholder
/// </summary>
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


    public void Death() //Destruye la defensa y cambia el estado del placeholder a Sin Construir
    {
        BuildingManager.Instance.RemoveBuilding(gameObject);
        _placeholderState.SetState(BuildingStateMachine.BuildingState.NotBuilt);

        if(_defenseComponent.placeholder.TryGetComponent<SpecialPlaceholderComponent>(out SpecialPlaceholderComponent specialPh))
        {
            specialPh.PlaceholderDestroyed();
        }

        Destroy(gameObject);
    }
}
