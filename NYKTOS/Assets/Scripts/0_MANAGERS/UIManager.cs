using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager principal de la UI
/// </summary>
public class UIManager : MonoBehaviour
{
    // Instancia singleton de UIManager
    private static UIManager _instance;
    public static UIManager Instance
    {
        get { return _instance; }
    }

    #region references

    // Referencia a la pantalla de muerte
    [SerializeField]
    private GameObject _deathScreen;

    // Referencia al menú de pausa
    [SerializeField]
    private Canvas _pauseMenu;

    // Referencias a los textos que muestran la cantidad de cristales
    [SerializeField]
    private TMP_Text _textoCrystalA;

    [SerializeField]
    private TMP_Text _textoCrystalM;

    [SerializeField]
    private TMP_Text _textoCrystalC;

    // Referencias a las imágenes de las armas
    [SerializeField]
    private Image _weaponPalo;

    [SerializeField]
    private Image _weaponCetro;

    // Referencias a las imágenes de los corazones
    [SerializeField]
    private Image[] _hearts;

    // Referencias a los sprites de corazones llenos y medios
    [SerializeField]
    private Sprite _fullHeart;

    [SerializeField]
    private Sprite _halfHeart;

    // Referencia al inventario del jugador
    [SerializeField]
    private PlayerInventory _playerInventory;

    #endregion

    // Método para actualizar los textos de cristales desde el inventario
    private void UpdateFromInventory()
    {
        _textoCrystalA.text = _playerInventory.Amarillo.ToString();
        _textoCrystalC.text = _playerInventory.Cian.ToString();
        _textoCrystalM.text = _playerInventory.Magenta.ToString();
    }

    // Métodos para actualizar individualmente los textos de cristales
    public void CrystalA(int _numCrysA)
    {
        _textoCrystalA.text = _numCrysA.ToString();
    }

    public void CrystalM(int _numCrysM)
    {
        _textoCrystalM.text = _numCrysM.ToString();
    }

    public void CrystalC(int _numCrysC)
    {
        _textoCrystalC.text = _numCrysC.ToString();
    }

    // Método para mejorar el arma del jugador
    public void MejoraArma()
    {
        _weaponPalo.enabled = false;
        _weaponCetro.enabled = true;
    }

    // Método para actualizar los corazones de la UI según la salud del jugador
    public void Hearts(int health)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < health / 2 || (i < health / 2 + 1 && health % 2 != 0))
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }

            _hearts[i].sprite = _fullHeart;
        }

        if (health % 2 != 0)
        {
            _hearts[health / 2].sprite = _halfHeart;
        }
    }

    // Método para mostrar la pantalla de muerte
    public void DeathScreenOn()
    {
        _deathScreen.SetActive(true);
    }

    // Método para ocultar la pantalla de muerte
    public void DeathScreenOff()
    {
        _deathScreen.SetActive(false);
    }

    // Método Awake se llama cuando el script se instancia
    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }

    // Método Start se llama antes de la primera actualización del frame
    void Start()
    {
        _weaponPalo.enabled = true;
        UpdateFromInventory();

        _playerInventory.InventoryUpdate.AddListener(UpdateFromInventory);
    }

    
    void OnDestroy()
    {
        _playerInventory.InventoryUpdate.RemoveListener(UpdateFromInventory);
    }
}