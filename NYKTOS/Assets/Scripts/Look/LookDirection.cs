using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection : MonoBehaviour
{
    public Vector2 lookDirection = Vector2.down;
    private Animator _animator;

    [SerializeField] float _waitTime = 0.1f; //tiempo para el lerp de la animaci�n

    public void SetLookDirection(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            Wait(dir); 
            
            lookDirection = dir;
        }
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // m�todo 0para la animacion
    void Wait(Vector2 dir) 
    {
        dir = dir.normalized;
        _animator.SetFloat("xAxis", Mathf.Lerp(_animator.GetFloat("xAxis"), dir.x, _waitTime * Time.deltaTime));
        _animator.SetFloat("yAxis", Mathf.Lerp(_animator.GetFloat("yAxis"), dir.y, _waitTime * Time.deltaTime));
    }
}
