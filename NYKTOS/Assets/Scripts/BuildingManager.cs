using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    #region properties
    private GameObject _currentPlaceholder;

    public GameObject CurrentPlaceholder
    {
        get { return _currentPlaceholder; }
        set { _currentPlaceholder = value; }
    }

    [SerializeField]
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
    public void SetBuilding(GameObject building)
    {
        _selectedDefense = building;
    }

    private void BuildDefense()
    {
        Instantiate(_selectedDefense,_currentPlaceholder.transform.position,Quaternion.identity);
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
