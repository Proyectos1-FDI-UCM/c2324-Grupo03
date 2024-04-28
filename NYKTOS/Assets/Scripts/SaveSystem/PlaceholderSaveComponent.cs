using System.Collections;
using UnityEngine;

public class PlaceholderSaveComponent : CollaboratorWorker
{
    [SerializeField]
    private PlaceholderDefense _currentDefense = PlaceholderDefense.None; 
    public PlaceholderDefense CurrentDefense 
    { 
        set 
        { 
            _currentDefense = value;

            _saveData?.SetPlaceholderDefense(_placeholderId, _currentDefense);
        }
    }

    [SerializeField]
    private PlaceholderSaveData _saveData;

    private int _placeholderId = -1;

    protected override IEnumerator Perform()
    {
        // [Marco] 
        //
        // Soy consciente que esto es codigo duplicado, es lo que hay, 

        _currentDefense = _saveData.GetPlaceholderDefense(_placeholderId);
        
        yield return null;

        GameObject selectedDefense = null;

        if (_currentDefense != PlaceholderDefense.None)
        {
            switch (_currentDefense)
            {
                case PlaceholderDefense.Beacon:
                    selectedDefense = BuildingManager.Instance.Beacon;
                    break;
                case PlaceholderDefense.Turret:
                    selectedDefense = BuildingManager.Instance.Turret;
                    break;
                case PlaceholderDefense.Wall:
                    selectedDefense = BuildingManager.Instance.Wall;
                    break;
            }
            
            if (_currentDefense == PlaceholderDefense.Turret)
            {
                Debug.Log($"{selectedDefense} - {selectedDefense.transform.position} - {transform.position}");
                selectedDefense.transform.position = new Vector2(transform.position.x, transform.position.y + BuildingManager.Instance.OffsetNotWall);
            }
            else
            {
                Debug.Log($"{selectedDefense} - {selectedDefense.transform.position} - {transform.position}");
                selectedDefense.transform.position = new Vector2(transform.position.x, transform.position.y + BuildingManager.Instance.OffsetNotWall / 2);
            }

            yield return null;

            GameObject defense = Instantiate(selectedDefense, selectedDefense.transform.position, Quaternion.identity);
            defense.GetComponent<DefenseComponent>().placeholder = gameObject;

            yield return null;

            BuildingManager.Instance.AddBuilding(defense);

            BuildingStateMachine placeholderState = GetComponent<BuildingStateMachine>();
            placeholderState.SetState(BuildingStateMachine.BuildingState.Built);
            placeholderState.isInteractable = false;
            GetComponent<InteractableObjects>().ShowInteraction(placeholderState.isInteractable);

            if(TryGetComponent<SpecialPlaceholderComponent>(out SpecialPlaceholderComponent specialPh))
            {
                specialPh.PlaceholderBuilt();
            }
            yield return null;
        }
        yield return null;
    }

    void OnValidate()
    {
        if(_saveData != null)
        {
            if (_placeholderId == -1)
            {
                _placeholderId = _saveData.AddPlaceholder(_currentDefense);
            }
            else
            {
                _saveData.SetPlaceholderDefense(_placeholderId, _currentDefense);
            }
        }
    }
}