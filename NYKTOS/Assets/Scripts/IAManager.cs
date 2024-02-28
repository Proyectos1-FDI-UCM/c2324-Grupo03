using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class IAManager : MonoBehaviour
{
    static private IAManager _instance;
    public static IAManager Instance
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

    //[SerializeField]
    //public enum Enemy { Vespertilio, Vitae, Poly, Celer, AraneaM, AraneaH, Uge };
    //[SerializeField]
    //public enum Priority { Player, Defense, Altar, Nexus };

    [SerializeField]
    private GameObject _player;
    public GameObject player { get { return _player; } }

    [SerializeField]
    private GameObject[] Defenses;
    [SerializeField]
    private GameObject[] Altars;
    //[SerializeField]
    //private GameObject Nexus;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
