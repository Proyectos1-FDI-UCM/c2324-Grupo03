using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAraneasBehaviour : MonoBehaviour, IBehaviour 
{
    [SerializeField] private GameObject _araneaHijaPrefab;
    private Transform _araneaMadreTransform;
    [SerializeField] private int _maxHijas = 3;
    private int _currentHijas;
    public void PerformBehaviour() 
    {
        if (_currentHijas < _maxHijas)
        {
            _currentHijas++;
            SpawnSpider();
        }

    }
    private void SpawnSpider() {
        if (_araneaHijaPrefab != null && _araneaMadreTransform != null) {
            Instantiate(_araneaHijaPrefab, _araneaMadreTransform.position, Quaternion.identity);
        } else {
            Debug.LogWarning("No hay prefab");
        }
    }

    private void Awake() {
        _araneaMadreTransform = GetComponentInParent<Transform>();
    }
}
