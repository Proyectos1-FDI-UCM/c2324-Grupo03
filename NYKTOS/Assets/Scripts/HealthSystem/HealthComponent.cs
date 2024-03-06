using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    #region references

    [SerializeField]
    private UIManager _UIManager;
    [SerializeField]
    private IDeath _deathComponent;

    private PlayerController _playerController;
    
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
        _deathComponent = GetComponent<IDeath>();

        _playerController = GetComponent<PlayerController>();
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

                // Cosa marco, me he cargado el metodo morir
                _deathComponent.Death();
            }
            
            Invoke(nameof(DisableInm), _inmTime);
            if(_playerController != null)
            {
                _UIManager.Hearts(_currentHealth);
            }
            
        }

    }

    public void Heal(int heal)
    {
        _currentHealth += heal;


        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        if( _playerController != null)
        {
            _UIManager.Hearts(_currentHealth);
        }
        
    }

    void DisableInm()
    {
        _inmune = false;
    }
}