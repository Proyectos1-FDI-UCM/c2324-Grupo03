using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationBehaviour : MonoBehaviour, IBehaviour
{

    private Rigidbody2D _rigidbody;
    private enum AnimationType
    {
        Attacking, Walking, Dying, SpawnHijas
    }
    [SerializeField]
    private AnimationType _animationType;
    private Animator _animator;

    public void PerformBehaviour()
    {
        if (_animator != null) {
            if (_animationType == AnimationType.Attacking) _animator.Play("Attacking");
            else if (_animationType == AnimationType.Walking && !isOnCoolDown) StartCoroutine(Walk());
            //else if (_animationType == AnimationType.Dying) 
            else if (_animationType == AnimationType.SpawnHijas) {
                Debug.Log("Spawneando Hijas");
                _animator.Play("SpawnHijas");

            }
        } else {
            Debug.Log(this.name + "No ha encontrado el animator");
        }
    }

    void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        _animator = GetComponentInParent<HealthComponent>().GetComponentInChildren<Animator>();
    }

    private bool isOnCoolDown=false;
    private IEnumerator Walk() 
    {
        isOnCoolDown = true;
        Vector2 velocity = _rigidbody.velocity;
        Vector2 movementDirection = velocity.normalized;
        _animator.SetFloat("xAxis", movementDirection.x);
        _animator.SetFloat("yAxis", movementDirection.y);

        yield return new WaitForSeconds(0.5f);
        isOnCoolDown = false;
    }
}
