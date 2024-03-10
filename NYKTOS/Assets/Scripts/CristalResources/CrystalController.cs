using UnityEngine;
using UnityEngine.Rendering;

//Codigo de Iker :D
public class CrystalController : MonoBehaviour
{
    private Animator _myAnimator;
    //SERVIRA PARA HACER LA REFERENCIA CON QUE TIPO DE CRISTAL HEMOS TOCADO
    [SerializeField]
    private CrystalBag _crystalBag;
    private GameObject _crystalPrefab;
    [SerializeField]
    private GameObject _player;
    private Collider2D _AttractionArea;
    //CORREGIR PARA QUE SE ATRAIGA CON UN BOOL PRODUCIDO POR EL TRIGGERCOLLIDER DE UN AREA DE UN GAMEOBJECT HIJO DEL PLAYER, PARA SEPARAR COLLIDERS
    private bool _Atracted;
    private float dropForce = 1f;
    private bool ObtainedCrystal = false;
    [SerializeField]
    private PlayerInventory inventory;

    void Start()
    {
        //CORREGIR
        _crystalBag = FindObjectOfType<CrystalBag>();
        _crystalPrefab = gameObject;
        //CORREGIR
        _player = FindObjectOfType<PlayerController>().gameObject;
        _myAnimator = GetComponent<Animator>();
        _AttractionArea = GetComponentInChildren<Collider2D>();
    }

    void Update()
    {
        //LA ATRACCION POR EL MOMENTO ES AUTOMATICA, SE NECESITA HACER CON LA CONDICIÓN
        if (_AttractionArea != null)
        {
            _myAnimator.enabled = false;
            Vector2 DirectionToPlayer = _player.transform.position - transform.position;
            _crystalPrefab.GetComponent<Rigidbody2D>().AddForce(DirectionToPlayer * dropForce,ForceMode2D.Impulse);
        }

        if (ObtainedCrystal)
        {
            Destroy(_crystalPrefab);
            if (inventory != null)
            {
                //SOLO RECOGE CRISTALES AMARILLOS DE MOMENTO
                inventory.Amarillo++;
                inventory.OnValidate();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            ObtainedCrystal = true;
        }
    }
}
