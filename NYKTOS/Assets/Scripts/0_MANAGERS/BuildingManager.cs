using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager _instance;
    public static BuildingManager Instance
    {
        get { return _instance; }
    }

    #region references
    [SerializeField]
    private PlayerInventory _inventory;
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
    #endregion

    #region parameters
    private float _offsetNotWall = 0.9f;

    [SerializeField]
    private int _beaconPrice = 0;

    [SerializeField]
    private int _wallPrice = 0;

    [SerializeField]
    private int _turretPrice = 0;
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
            _selectedDefense.transform.position = new Vector2(_currentPlaceholder.transform.position.x, _currentPlaceholder.transform.position.y + _offsetNotWall/2);
        }

        GameObject defense = Instantiate(_selectedDefense,_selectedDefense.transform.position,Quaternion.identity);
        defense.GetComponent<DefenseComponent>().placeholder = _currentPlaceholder;

        AddBuilding(defense);

        BuildingStateMachine placeholderState = _currentPlaceholder.GetComponent<BuildingStateMachine>();
        placeholderState.SetState(BuildingStateMachine.BuildingState.Built);
        placeholderState.isInteractable = false;

        MenuManager.Instance.CloseAllMenus();
    }
    
    public void BuildBeacon()
    {
        // El faro cuesta x cristales amarillos
        if (_inventory.Amarillo >= _beaconPrice)
        {
            _inventory.Amarillo -= _beaconPrice;
            SetBuilding(_beacon);
            BuildDefense();
        }
        // else => Mensaje de no suficientes cristales? -> Por eventos!
    }    
    
    public void BuildWall()
    {
        if(_inventory.Amarillo >= _wallPrice)
        {
            _inventory.Amarillo -= _wallPrice;
            SetBuilding(_wall);
            BuildDefense();
        }
        // else => Mensaje de no suficientes cristales?
    }

    public void BuildTurret()
    {
        if(_inventory.Amarillo >= _turretPrice)
        {
            _inventory.Amarillo -= _turretPrice;
            SetBuilding(_turret);
            BuildDefense();
        }
        // else => Mensaje de no suficientes cristales?
    }
    #endregion



    #endregion

    #region buildingArray
    public List<GameObject> buildingArray { get { return _buildingArray; } }
    private List<GameObject> _buildingArray = new List<GameObject>();

    public void AddBuilding(GameObject obj)
    {
        print(obj);
        _buildingArray.Add(obj);
    }

    public void RemoveBuilding(GameObject obj)
    {
        _buildingArray.Remove(obj);
    }
    #endregion

    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }

}
