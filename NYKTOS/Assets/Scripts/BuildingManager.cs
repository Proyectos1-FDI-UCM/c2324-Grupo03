using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    #region properties
    [SerializeField]
    private GameObject _currentPlaceholder;

    public GameObject CurrentPlaceholder
    {
        get { return _currentPlaceholder; }
        set { _currentPlaceholder = value; }
    }

    private GameObject _selectedDefense;
    public GameObject selectedDefense 
    { get { return _selectedDefense; } 
      set { _selectedDefense = value; } 
    }

    private HealthComponent _healthComponent;
    #endregion

    #region parameters
    private float _offsetNotWall = 0.9f;
    private int _healthimposter = 40;
    private float crono = 3f;
    private bool UnaVez = true;
    #endregion

    #region building prefabs
    [SerializeField]
    private GameObject _beacon;

    [SerializeField]
    private GameObject _wall;

    [SerializeField]
    private GameObject _turret;
    #endregion

    #region methods
    private void SetBuilding(GameObject building)
    {
        _selectedDefense = building;
    }

    private void BuildDefense()
    {
        if (_selectedDefense == _beacon || _selectedDefense == _turret)
        {
            _selectedDefense.transform.position = new Vector2(_currentPlaceholder.transform.position.x, _currentPlaceholder.transform.position.y + _offsetNotWall);
        }
        else
        {
            _selectedDefense.transform.position = _currentPlaceholder.transform.position;
        }

        GameObject defense = Instantiate(_selectedDefense,_selectedDefense.transform.position,Quaternion.identity);
        defense.GetComponent<DefenseComponent>().placeholder = _currentPlaceholder;

        _currentPlaceholder.GetComponent<BuildingStateMachine>().SetState(BuildingStateMachine.BuildingState.Built);
        //_healthComponent = _selectedDefense.GetComponent<HealthComponent>();
        _currentPlaceholder.GetComponent<PlaceholderComponent>().CloseMenu();
    }

    private void DestroyDefense()
    {
        _currentPlaceholder.GetComponent<BuildingStateMachine>().SetState(BuildingStateMachine.BuildingState.NotBuilt);
        //_healthComponent.Damage(_healthimposter);
    }
    public void BuildTurret()
    {
        SetBuilding(_turret);
        BuildDefense();
    }    
    
    public void BuildBeacon()
    {
        SetBuilding(_beacon);
        BuildDefense();
    }    
    
    public void BuildWall()
    {
        SetBuilding(_wall);
        BuildDefense();
    }


    #endregion


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        crono -= Time.deltaTime;
        if ( crono <= 0 && UnaVez)
        {
            DestroyDefense();
            Debug.Log("Destrusion");
            UnaVez = false;
        }
        */
    }
}
