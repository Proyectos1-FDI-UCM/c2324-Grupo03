using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAraneasBehaviour : MonoBehaviour, IBehaviour 
{

    [SerializeField] private GameObject _araneaHijaPrefab;
    private Transform _araneaMadreTransform;
    public float spawnInterval = 5f; 
    private float timer = 0f;
    [SerializeField] private int _maxHijas = 3;
    private int _currentHijas;
    public void PerformBehaviour() 
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && _currentHijas< _maxHijas) 
        {
            SpawnSpider(); // Spawnear una araña hija
            timer = 0f;
            _currentHijas++;
            Debug.Log("Hijas: " + _currentHijas);
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
