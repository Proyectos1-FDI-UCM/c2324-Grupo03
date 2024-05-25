using UnityEngine;

/// <summary>
/// Establece la dirección a la que mira/apunta el jugador
/// </summary>
public class LookDirection : MonoBehaviour
{
    public Vector2 lookDirection = Vector2.down;
    private Animator _animator;

    [SerializeField] float _waitTime = 0.1f; //tiempo para el lerp de la animación

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

    /// <summary>
    /// metodo que se encarga de decir la direccion al animator para que el sprite de hemera siga al raton
    /// </summary>
    /// <param name="dir"> Vector de donde el player esta mirando </param>
    void Wait(Vector2 dir) 
    {
        dir = dir.normalized;
        _animator.SetFloat("xAxis", Mathf.Lerp(_animator.GetFloat("xAxis"), dir.x, _waitTime * Time.deltaTime));
        _animator.SetFloat("yAxis", Mathf.Lerp(_animator.GetFloat("yAxis"), dir.y, _waitTime * Time.deltaTime));
    }
}
