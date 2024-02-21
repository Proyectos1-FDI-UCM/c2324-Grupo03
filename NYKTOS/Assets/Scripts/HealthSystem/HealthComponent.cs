using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private int _maxHealth = 6;
    [SerializeField]
    private int _currentHealth;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        
    }

    void Damage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Muerte();
        }
    }

    void Heal (int heal)
    {
        _currentHealth += heal;

        if(_currentHealth >_maxHealth) 
        {
            _currentHealth = _maxHealth;
        }
    }

    void Muerte()
    {

    }
}


   
   
