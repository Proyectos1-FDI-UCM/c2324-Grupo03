using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    #region references

    [SerializeField]
    private UIManager _UIManager;
    private EnemyDeath _enemyDeath;
    private PlayerDeath _playerDeath;
    private BuildingDeath _buildingDeath;
    
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

    #region events
    

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        //MAria
        _enemyDeath = GetComponent<EnemyDeath>();
        _playerDeath = GetComponent<PlayerDeath>();
        //Iker
        _buildingDeath = GetComponent<BuildingDeath>();
        
    }

    private void Update()
    {

    }

    public int GetHealth()
    {
        return _maxHealth;
    }

    public void Damage(int damage)
    {
        if (!_inmune)
        {
            _currentHealth -= damage;
            _inmune = true;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Muerte();
            }
            
            Invoke(nameof(DisableInm), _inmTime);
            _UIManager.Hearts(_currentHealth);
        }

    }

    public void Heal(int heal)
    {
        _currentHealth += heal;


        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        _UIManager.Hearts(_currentHealth);
    }

    void Muerte()
    {
        if (GetComponent<EnemyDeath>() != null)
        {
            _enemyDeath.Die();
        }
        else if (GetComponent<PlayerDeath>() != null)
        {
            _playerDeath.Death();
        }
        else if (GetComponent<BuildingDeath>() != null)
        {
            _buildingDeath.DestroyBuilding();
        }
    }

    void DisableInm()
    {
        _inmune = false;
    }
}


   
   
