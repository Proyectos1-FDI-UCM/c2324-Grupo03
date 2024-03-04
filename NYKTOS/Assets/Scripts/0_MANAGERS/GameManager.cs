using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    static private GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //Esto es para acceder al player globalmente
    [SerializeField]
    private GameObject _player;
    public GameObject player { get { return _player; } }

    //Esto es para crear un evento dependiendo de si todos los altares son construidos o destruidos
    [SerializeField]
    private GameObject _altar;
    public GameObject altar { get { return _altar; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
