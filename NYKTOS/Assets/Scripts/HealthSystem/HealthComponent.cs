using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region references
    private EnemyDeath _enemyDeath;
    #endregion
    #region parameters
    [SerializeField]
    private int _maxHealth = 6;
    [SerializeField]
    private int _currentHealth;
    private bool _inmune = false;
    [SerializeField]
    private float _inmTime;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        //MAria
        _enemyDeath = GetComponent<EnemyDeath>();
    }

    private void Update()
    {

    }

    public void Damage(int damage)
    {
        if (!_inmune)
        {
            _currentHealth -= damage;
            _inmune = true;

            if (_currentHealth <= 0)
            {
                Muerte();
            }

            Invoke(nameof(DisableInm), _inmTime);
        }

    }

    public void Heal(int heal)
    {
        _currentHealth += heal;


        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    void Muerte()
    {
        if (GetComponent<EnemyDeath>() != null)
        {
            _enemyDeath.Die();
        }
    }

    void DisableInm()
    {
        _inmune = false;
    }
}


   
   
