using UnityEngine;

// No se usa

[CreateAssetMenu(fileName = "New SelectedDefense", menuName = "SelectedDefense")]
public class SelectedDefense : ScriptableObject
{
    [SerializeField]
    private Defensetype _selectedDefense;

    public Defensetype selectedDefense
    {
        get { return _selectedDefense; }
        set { _selectedDefense = value; }
    }
    
}
