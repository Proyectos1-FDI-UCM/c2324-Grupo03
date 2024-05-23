using UnityEngine;

/// <summary>
/// Al destruir una defensa, �sta se quita de la lista del Building Manager 
/// y se actualiza el estado de construcci�n del placeholder
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


    public void Death()
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
