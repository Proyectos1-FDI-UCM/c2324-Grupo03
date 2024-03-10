using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//Codigo de Iker :D
public class CrystalController : MonoBehaviour
{
    private Animator _myAnimator;
    //Se hace la referencia para cada clon desde CrystalBag
    private CrystalBag _crystalBag;
    private GameObject _crystalPrefab;
    [SerializeField]
    private GameObject _player;
    private bool _Atracted;
    private float dropForce = 1f;
    private bool ObtainedCrystal = false;

    private float MinimalDistanceToObtain = 0.3f;

    [SerializeField]
    private PlayerInventory inventory;
    //private string CrystalName;
    void Start()
    {
        _crystalPrefab = gameObject;
        //CORREGIR
        //_crystalBag = FindObjectOfType<CrystalBag>();
        _player = FindObjectOfType<PlayerController>().gameObject;
        _myAnimator = GetComponent<Animator>();
        /*
        ResourceCrystal TypeofCrystal = GetComponent<ResourceCrystal>();
        CrystalName = TypeofCrystal.CristalName;
        */

    }

    void Update()
    {
        float DistanceToPlayerNumber = Vector2.Distance(_player.transform.position, transform.position);

        if (_Atracted)
        {
            _myAnimator.enabled = false;
            Vector2 DirectionToPlayer = _player.transform.position - transform.position;
            _crystalPrefab.GetComponent<Rigidbody2D>().AddForce(DirectionToPlayer * dropForce,ForceMode2D.Impulse);
        }

        if (DistanceToPlayerNumber <= MinimalDistanceToObtain)
        {
            ObtainedCrystal = true;
        }

        if (ObtainedCrystal)
        {
            inventory.Amarillo++;
            inventory.OnValidate();
            Destroy(_crystalPrefab);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            Debug.Log("Encontré al jugador!");
            _Atracted = true;
        }
    }

    /*
    private void WhatCrystal()
    {
        if (_crystalBag == null)
        {
            Debug.Log("no tengo mochilita");
        }
        
        _crystalBag.WhatCrystalITook();
    }
    */

}
