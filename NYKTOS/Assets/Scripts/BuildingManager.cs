using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private MenuManager _menuManager;

    #endregion

    #region properties
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

    #region build defenses
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

        AddBuilding(defense);

        _currentPlaceholder.GetComponent<BuildingStateMachine>().SetState(BuildingStateMachine.BuildingState.Built);
        //_healthComponent = _selectedDefense.GetComponent<HealthComponent>();
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

    public void StartNight()
    {
        _menuManager.CloseMenu();
        // Que se llame al manager encargado de cambiar la noche
        GameManager.Instance.UpdateGameState(GameState.Night);
    }

    #endregion

    #region buildingArray
    /// <summary>
    /// Array que contiene todos los gameObjects de edificios. 
    /// ¡ATENCION! Su longitud debe ser BuildingManager.buildingNumber
    /// </summary>
    public GameObject[] buildingArray { get { return _buildingArray; } }
    private GameObject[] _buildingArray;

    private int _placeholderNumber = 0;
    public int buildingNumber { get { return _buildingNumber; } }
    private int _buildingNumber = 0;

    public void IncreasePlaceholderNumber()
    {
        _placeholderNumber++;
    }

    private void AddBuilding(GameObject _object)
    {
        _buildingArray[_buildingNumber] = _object;
        _buildingNumber++;
    }

    public void RemoveBuilding(GameObject _object)
    {
        bool found = false;
        int i = 0; //la posicion del edificio encontrado
        while (i< _buildingNumber && !found)
        {
            found = _object == _buildingArray[i];
            if (!found) i++;
        }

        for (; i< _buildingNumber-1; i++)
        {
            _buildingArray[i] = _buildingArray[i+1];
        }

        _buildingNumber--;
    }

    private void Awake()
    {
        _buildingArray = new GameObject[_placeholderNumber];
    }

    #endregion
}
