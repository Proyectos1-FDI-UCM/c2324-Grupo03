using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject _sprite;

    [SerializeField]
    private GameObject _collider;
    private string _name;

    private BuildingStateMachine _buildingState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _name && _buildingState.isInteractable) _sprite.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == _name) _sprite.SetActive(false);
    }

    private void Start()
    {
        _name = _collider.name;
        _buildingState = GetComponent<BuildingStateMachine>();
    }
}
