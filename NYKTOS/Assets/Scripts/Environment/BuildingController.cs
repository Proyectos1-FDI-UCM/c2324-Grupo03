using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour, IInteractable
{
    #region references
    
    #endregion
    public void Interact()
    {
        if (TryGetComponent(out IBuilding building)) building.OpenMenu();
    }
}
