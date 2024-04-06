using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    #region references

    private HealthBar healthBar;
    private GameObject HealthBarGameObject;

    private IDeath _deathComponent;

    private PlayerController _playerController;
    private DefenseComponent _defenseComponent;
    private bool _isDefense = false;
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
    [SerializeField] UnityEvent _OnHurt;
    [SerializeField] UnityEvent _OnDying;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        
        _deathComponent = GetComponent<IDeath>();

        _playerController = GetComponent<PlayerController>();

        healthBar = GetComponentInChildren<HealthBar>();

        if (healthBar != null)
        {
            HealthBarGameObject = GetComponentInChildren<HealthBar>().gameObject;
            HealthBarGameObject.SetActive(false);
        }

    }

    public void Damage(int damage)
    {

        if (!_inmune)
        {
            _OnHurt?.Invoke();
            _currentHealth -= damage;
            _inmune = true;
            if (HealthBarGameObject != null)
            {
                HealthBarGameObject.SetActive(true);
            }

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;

                _OnDying?.Invoke();
                // Cosa marco, me he cargado el metodo morir
                _deathComponent.Death();
            }
            
            Invoke(nameof(DisableInm), _inmTime);
            if(_playerController != null)
            {
                UIManager.Instance.Hearts(_currentHealth);
            }
            
        }

        if (healthBar != null && HealthBarGameObject != null)
        {
            healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
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
            UIManager.Instance.Hearts(_currentHealth);
        }
        
    }

    public void MaxHealth()
    {
        _currentHealth = _maxHealth;
        UIManager.Instance.Hearts(_currentHealth);
        if (HealthBarGameObject != null)
        {
            HealthBarGameObject.SetActive(false);
        }
    }

    void DisableInm()
    {
        _inmune = false;
    }

}