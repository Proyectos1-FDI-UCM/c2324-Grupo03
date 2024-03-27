using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject _sprite;

    [SerializeField]
    private GameObject _object;
    private string _name;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _name)
        {
            _sprite.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == _name) _sprite.SetActive(true);
    }

    private void Start()
    {
        _name = _object.name;
        Debug.Log(_name);
    }
}
