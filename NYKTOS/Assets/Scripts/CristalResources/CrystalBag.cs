using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using UnityEditor;
using UnityEngine;


//Codigo de Iker :D
public class CrystalBag : MonoBehaviour
{
    private float dropForce = 20f;
    private bool ObtainedCrystal = false;
    [SerializeField]
    private PlayerInventory inventory;
    //public Collider2D _playerCollider;
    //Necesitamos el CrystalPrefab aquí
    public GameObject droppedItemPrefab;
    private ResourceCrystal _droppedCrystal;
    public ResourceCrystal droppedCrystal
    {
        get { return _droppedCrystal; }
    }
    private GameObject crystalGameObject;
    //En la lista de Cristales ponemos todos los cristales creados con el ScriptableObject CrystalResources
    public List<ResourceCrystal> CrystalList = new List<ResourceCrystal>();
    private List<GameObject> clonedCrystals = new List<GameObject>();

    [SerializeField]
    private SpawnManager _spawnManager;

    private int amarillosObligatorios;
    private int cianesObligatorios;
    private int magentasObligatorios;
    private int probAmarillo;
    private int probCian;
    private int probMagenta;
    private bool RecursosObligatorios = true;

    private int sumaCristalesObligatorios;

    //Esto usa la logica del porcentaje, toma un numero aleatorio entre 1 y 100.
    //Se recopila la lista de cristales con su probabilidad que puede soltar el enemigo.
    //Dependiendo del tipo de enemigo podemos poner los cristales que nosotros queramos que dropee //FALTA HACER CAMBIO DE PROBABILIDAD
    //Entonces para cada Crystal de la lootList toma una condición en la que si el numero random es menor que la probabilidad puesta, se añade ese cristal.
    ResourceCrystal GetDroppedCrystals()
    {
        int RandomNumber = Random.Range(1, 101);
        List<ResourceCrystal> possibleCrystal = new List<ResourceCrystal>();
        // EN CRYSTALLIST
        // 0 AMARILLO
        // 1 MAGENTA
        // 2 CIAN

        if (amarillosObligatorios == 0 && cianesObligatorios == 0 && magentasObligatorios == 0) RecursosObligatorios = false;

        if (RecursosObligatorios)
        {
            Debug.Log("Recurso obligatorio");
            if (amarillosObligatorios > 0)
            {
                if (RandomNumber <= probAmarillo /*|| cianesObligatorios == 0 && magentasObligatorios == 0*/)
                {
                    possibleCrystal.Add(CrystalList[0]);
                    _spawnManager.CurrentRequiredYellow--;
                    amarillosObligatorios--;
                }
            }
            else if (cianesObligatorios > 0 /*|| amarillosObligatorios == 0 && magentasObligatorios == 0*/)
            {
                if (RandomNumber <= probCian)
                {
                    possibleCrystal.Add(CrystalList[2]);
                    _spawnManager.CurrentRequiredCyan--;
                    cianesObligatorios--;
                }
            }
            else if (magentasObligatorios > 0 /*|| amarillosObligatorios == 0 && cianesObligatorios == 0*/)
            {
                if (RandomNumber <= probMagenta)
                {
                    possibleCrystal.Add(CrystalList[1]);
                    _spawnManager.CurrentRequiredMagenta--;
                    magentasObligatorios--;
                }
            }
            else
            {
                possibleCrystal.Add(CrystalList[0]);
            }
        } 
        else
        {
            /*
            Debug.Log("Recurso aleatorio");
            foreach (ResourceCrystal item in CrystalList)
            {
                if (RandomNumber <= item.dropChance) possibleCrystal.Add(item);
            }
            */
            print(RandomNumber);
            print(probMagenta);
            print(probCian);
            if (RandomNumber <= probCian) possibleCrystal.Add(CrystalList[1]);
            else if (RandomNumber <= probMagenta) possibleCrystal.Add(CrystalList[2]);
            else possibleCrystal.Add(CrystalList[0]);


        }

        //Esto es para que elija solo un drop para tirar entre todos
        if (possibleCrystal.Count > 0)
        {
            ResourceCrystal droppedCrystal = possibleCrystal[Random.Range(0,possibleCrystal.Count)];
            return droppedCrystal;
        }
        else
        {
            Debug.Log("No Crystal Dropped");
            return null;
        }
    }

    public void InstantiateCrystal(Vector3 spawnPosition)
    {
        _droppedCrystal = GetDroppedCrystals();
        if (_droppedCrystal != null)
        {
            crystalGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            crystalGameObject.GetComponent<SpriteRenderer>().sprite = droppedCrystal.CristalSprite;
            clonedCrystals.Add(crystalGameObject);
            int DissapearTime = _droppedCrystal.dissapearTime;
            Destroy(crystalGameObject,DissapearTime);
        }
    }

    public void InteractWithCrystalClones()
    {
        foreach (GameObject clone in clonedCrystals)
        {
            GetComponent<CrystalBag>();
        }
    }

    private void Start()
    {
        //_spawnManager = SpawnManager.Instance;

        amarillosObligatorios = SpawnManager.Instance.CurrentRequiredYellow;
        cianesObligatorios = SpawnManager.Instance.CurrentRequiredYellow;
        magentasObligatorios = SpawnManager.Instance.CurrentRequiredYellow;
        probAmarillo = SpawnManager.Instance.ProbabilityYellow;
        probCian = SpawnManager.Instance.ProbabilyCyan;
        probMagenta = SpawnManager.Instance.ProbabilyMagenta;

        sumaCristalesObligatorios = amarillosObligatorios + cianesObligatorios + magentasObligatorios;
    }

    /*
    public void WhatCrystalITook()
    {
        foreach (ResourceCrystal crystal in CrystalList)
        {
            if (crystal.CristalName == "YC")
            {
                inventory.Amarillo++;
                inventory.OnValidate();
            } 
            else if (crystal.CristalName == "CC")
            {
                inventory.Cian++;
                inventory.OnValidate();
            }
            else if (crystal.CristalName == "MC")
            {
                inventory.Magenta++;
                inventory.OnValidate();
            }

        }
    }
    
    
    public bool YellowCrystal(bool Yellow)
    {
        return Yellow;
    }

    public bool CyanCrystal(bool Cyan)
    {
        return Cyan;
    }

    public bool MagentaCrystal(bool Magenta)
    {
        return Magenta;
    }
    */
}
