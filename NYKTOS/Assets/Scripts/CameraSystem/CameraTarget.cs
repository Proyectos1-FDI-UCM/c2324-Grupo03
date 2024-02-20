using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] 
    private new Camera camera;
    [SerializeField] 
    private Transform _player;
    [SerializeField] 
    private float limit;

    private Transform _myTransform;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = (_player.position + mousePosition) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -limit + _player.position.x, limit + _player.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -limit + _player.position.y, limit + _player.position.y);

        _myTransform.transform.position = targetPosition;
    }
}
