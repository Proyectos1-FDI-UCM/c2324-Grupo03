using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAnimation : MonoBehaviour {
    private float nextActionTime = 0.0f;
    private float period = 0.5f;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform _myTransform;
    private EnemyPriorityComponent _priorityComponent;

    public void Idle(Vector2 movdirection) {
        _animator.SetFloat("xAxis", movdirection.x);
        _animator.SetFloat("yAxis", movdirection.y);
    }

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _myTransform = GetComponent<Transform>();
        _priorityComponent = GetComponentInParent<EnemyPriorityComponent>();
    }
    void Update() {
        // Get the velocity vector of the Rigidbody
        Vector2 velocity = _rigidbody.velocity;
        float distanceToPlayer = Vector3.Magnitude(PlayerController.playerTransform.position - _myTransform.position);
        float distanceToBuild = Vector3.Magnitude(_priorityComponent.toNearestBuildingPath.corners[_priorityComponent.toNearestBuildingPath.corners.Length-1] - _myTransform.position);
        // Calculate the direction of movement (normalized vector)
        Vector2 movementDirection = velocity.normalized;
        if (Time.time >= nextActionTime) {
            Idle(movementDirection);
            nextActionTime += period;
        }
        if (distanceToPlayer < 1f || distanceToBuild <1f) {
            _animator.Play("Attacking");
        
        }
        
    }

}
//MARIA