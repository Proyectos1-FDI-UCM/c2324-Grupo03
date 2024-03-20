using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    private Transform _playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = _player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
