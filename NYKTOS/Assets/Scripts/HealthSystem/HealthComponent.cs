using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script que controla la vida de las entidades del juego
/// </summary>

public class HealthComponent : MonoBehaviour
{
    #region references

    private HealthBar healthBar;
    private GameObject HealthBarGameObject;

    private IDeath _deathComponent;

    private PlayerController _playerController;
    private PlayerAnimations _playerAnimations;
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

    #region testing
    [SerializeField]
    private bool alwaysInmune = false;
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
        _playerAnimations = GetComponentInChildren<PlayerAnimations>();

        if (healthBar != null)
        {
            HealthBarGameObject = GetComponentInChildren<HealthBar>().gameObject;
            HealthBarGameObject.SetActive(false);
        }

    }

    public void Damage(int damage) //Controla si se hace da�o a la entidad (no tiene inmunidad) y la cantidad de da�o que se hace.
    {

        if (!_inmune)
        {
            _OnHurt?.Invoke();
            if (!alwaysInmune) _currentHealth -= damage;
            _inmune = true;
            if (HealthBarGameObject != null)
            {
                HealthBarGameObject.SetActive(true);
            }

            if (_currentHealth <= 0) //Llama a la muerte de la entidad si se queda sin puntos de vida.
            {
                _currentHealth = 0;
                if (_playerAnimations != null) {_playerAnimations.StartDie();}
                _OnDying?.Invoke();
                _deathComponent.Death();
            }
            
            Invoke(nameof(DisableInm), _inmTime);
            if(_playerController != null)
            {
                UIManager.Instance.Hearts(_currentHealth);
                if (_playerAnimations != null) { _playerAnimations.StartHurt(); }
                
            }
            
        }

        if (healthBar != null && HealthBarGameObject != null)
        {
            healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
        

    }

    public void Heal(int heal) //Controla la cantidad de vida que se recupera y que esta no sobrepase el l�mite de vida de la entidad
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

    public void MaxHealth() //Recupera toda la vida del jugador
    {
        _currentHealth = _maxHealth;

        if (GetComponent<PlayerController>() != null) 
        {
            UIManager.Instance.Hearts(_currentHealth);

            if (HealthBarGameObject != null)
            {
                HealthBarGameObject.SetActive(false);
            }
        }
    }

    void DisableInm() 
    {
        _inmune = false;
    }

}