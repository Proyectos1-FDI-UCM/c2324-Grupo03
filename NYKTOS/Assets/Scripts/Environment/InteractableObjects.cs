using UnityEngine;

/// <summary>
/// Todos los objetos interactuables del juego muestran un s�mbolo de interacci�n cuando el jugador est� cerca
/// Este script activa/desactiva dicho s�mbolo
/// </summary>
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

    public void ShowInteraction(bool value)
    {
        if (value) _sprite.SetActive(true);
        else _sprite.SetActive(false);
    }

    private void Start()
    {
        _name = _collider.name;
        _buildingState = GetComponent<BuildingStateMachine>();
    }
}
