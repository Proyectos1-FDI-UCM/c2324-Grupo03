using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//Codigo de Iker :D
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

    //Referenciamos al player con un accesor
    [SerializeField]
    private GameObject _player;
    public GameObject player { get { return _player; } }

    //Ponemos las defensas y altares del nivel
    //En el tutorial, solo se tendrán en cuenta 2 defensas y 1 altar
    //En el juego normal, se tendrán en cuenta 18 defensas, 2 altares y 1 nexo
    //Habría que adaptarlo para que dependiendo del gameManager detecte si estamos en el tutorial o en el juego completo
    [SerializeField]
    private GameObject[] Defenses;
    [SerializeField]
    private GameObject[] Altars;
    //[SerializeField]
    //private GameObject Nexus;


    //Referenciamos el altar del tutorial con un accesor, sería el prefab del altar de la posicion 0 del array de altares
    //Hay que encontrar un metodo para poder hacer esto con todas las defensas, altares y el nexo que necesitemos
    private GameObject _altartutorial;
    public GameObject altartutorial {  get { return _altartutorial; } }  


    // Start is called before the first frame update
    void Start()
    {
        //Identificamos el altar del tutorial el gameObject colocado en la posición 0 del array
         _altartutorial = Altars[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
