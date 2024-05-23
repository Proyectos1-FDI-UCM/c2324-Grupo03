using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actualiza la vida de la defensa
/// Guarda el placeholder en el que está construida
/// </summary>
public class DefenseComponent : MonoBehaviour
{


    [SerializeField] private VoidEmitter _healthRestore;

    [SerializeField] 
    private Defense Defense;
    private HealthComponent _health;

    private GameObject _placeholder;

    public GameObject placeholder
    {
        get { return _placeholder; }
        set { _placeholder = value; }
    }

    public int Health;
    public int MaxHealth;

    void Start()
    {
        _health = GetComponent<HealthComponent>();

        MaxHealth = Defense.health;
        Health = Defense.health;
        _healthRestore.Perform.AddListener(_health.MaxHealth);
    }

    void OnDestroy()
    {
        _healthRestore.Perform.RemoveListener(_health.MaxHealth);
    }
}
