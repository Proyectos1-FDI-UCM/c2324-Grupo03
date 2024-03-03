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
        Instantiate(_selectedDefense,_currentPlaceholder.transform.position,Quaternion.identity);
        _currentPlaceholder.GetComponent<BuildingStateMachine>().SetState(BuildingStateMachine.BuildingState.Built);
        _currentPlaceholder.GetComponent<PlaceholderComponent>().CloseMenu();
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
        
    }
}
