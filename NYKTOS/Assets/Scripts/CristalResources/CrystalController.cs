using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//Codigo de Iker :D
public class CrystalController : MonoBehaviour
{
    private Animator _myAnimator;
    private Transform _myTransform;
    [SerializeField]
    private float AttractionForce = 3f;
    //Se hace la referencia para cada clon desde CrystalBag
    private CrystalBag _crystalBag;
    private GameObject _crystalPrefab;
    private Transform _player;
    private bool _Atracted;
    private float dropForce = 1f;
    private bool ObtainedCrystal = false;

    private float MinimalDistanceToObtain = 0.3f;

    [SerializeField]
    List<ResourceCrystal> possibleCrystal = new List<ResourceCrystal>();

    [SerializeField]
    private PlayerInventory inventory;
    //private string CrystalName;
    void Start()
    {
        _crystalPrefab = gameObject;
        _player = PlayerController.playerTransform;
        _myAnimator = GetComponent<Animator>();
        _myTransform = transform;
    }

    void Update()
    {
        float DistanceToPlayerNumber = Vector2.Distance(_player.transform.position, _myTransform.position);
        Sprite spriteCrystal = _crystalPrefab.GetComponent<SpriteRenderer>().sprite;


        if (_Atracted)
        {
            _myAnimator.enabled = false;
            Vector3 playerposition = new Vector3(_player.transform.position.x,_player.transform.position.y,_player.transform.position.z);
            Vector3 DirectionToPlayer = playerposition - _myTransform.position;
            _myTransform.position += DirectionToPlayer * AttractionForce * Time.deltaTime;
            
            //Apuntes para el futuro, el enemigo que viene como proyectil con una fuera como esta puede quedar muy bien y se puede esquivar con facilidad
            //_crystalPrefab.GetComponent<Rigidbody2D>().AddForce(DirectionToPlayer * dropForce,ForceMode2D.Impulse);
        }

        if (DistanceToPlayerNumber <= MinimalDistanceToObtain)
        {
            ObtainedCrystal = true;
        }

        if (ObtainedCrystal)
        {    
            
            if (possibleCrystal[0].CristalSprite == spriteCrystal)
            {
                inventory.Amarillo++;
                inventory.OnValidate();
            }
            else if (possibleCrystal[1].CristalSprite == spriteCrystal)
            {
                inventory.Magenta++;
                inventory.OnValidate();
            }
            else if (possibleCrystal[2].CristalSprite == spriteCrystal)
            {
                inventory.Cian++;
                inventory.OnValidate();
            }
            
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
