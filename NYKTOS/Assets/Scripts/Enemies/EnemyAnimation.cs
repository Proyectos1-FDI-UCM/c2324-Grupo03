using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAnimation : MonoBehaviour {
    private float nextActionTime = 0.0f;
    private float period = 0.5f;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private bool _attacking = true;

    public void Idle(Vector2 movdirection) {
        _animator.SetFloat("xAxis", movdirection.x);
        _animator.SetFloat("yAxis", movdirection.y);
    }
    public void AttackingAni() {
        _animator.SetBool("Attacking", true);
    }

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update() {
        // Get the velocity vector of the Rigidbody
        Vector2 velocity = _rigidbody.velocity;

        // Calculate the direction of movement (normalized vector)
        Vector2 movementDirection = velocity.normalized;
        if (Time.time >= nextActionTime && !_attacking) {
            Idle(movementDirection);
            nextActionTime += period;
        } 
        if(_attacking)AttackingAni();
    }
}
//MARIA