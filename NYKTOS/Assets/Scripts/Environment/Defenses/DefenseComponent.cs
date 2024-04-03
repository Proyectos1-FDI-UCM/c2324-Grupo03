using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseComponent : MonoBehaviour
{
    //VA ACTUALIZANDO EL ESTADO EN EL QUE SE ENCUENTRA LA DEFENSA
    //ACTUALIZA LA CANTIDAD DE VIDA EN INT (ALTARHEALTHCOMPONENT)
    //CONSTRUCCION AUTOMATICA CON UN BOOLEANO
    //SI ESTA CONSTRUIDO, ATRAE ENEMIGOS, ACTIVA VIDA Y NO SE PUEDE INTERACTUAR CON EL HASTA QUE SEA DESTRUIDO (ALTARHEALTHCOMPONENT)
    //SI ESTA DESTRUIDO, YA NO ATRAE ENEMIGOS, DESACTIVA EL FACTOR VIDA SE PUEDE INTERACTUAR CON EL (EN ESTE COMPONENTE)

    [SerializeField] private GenericEmmiter _healthRestore;

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
}
