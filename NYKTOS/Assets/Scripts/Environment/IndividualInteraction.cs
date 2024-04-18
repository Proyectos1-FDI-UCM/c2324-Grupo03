using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualInteraction : MonoBehaviour
{
    [SerializeField]
    private BoolEmitter _canInteractEmitter;

    private BuildingStateMachine _state;

    private void CanInteract(bool value)
    {
        _state.isInteractable = value;
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();

        _canInteractEmitter.Perform.AddListener(CanInteract);
    }

}
