using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Security.Cryptography;
using Unity.VisualScripting;
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
    //Necesitamos el CrystalPrefab aqu�
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

    private GameplayManager _spawnManager;

    private bool RecursosObligatorios = true;

    //Esto usa la logica del porcentaje, toma un numero aleatorio entre 1 y 100.
    //Se recopila la lista de cristales con su probabilidad que puede soltar el enemigo.
    //Dependiendo del tipo de enemigo podemos poner los cristales que nosotros queramos que dropee //FALTA HACER CAMBIO DE PROBABILIDAD
    //Entonces para cada Crystal de la lootList toma una condici�n en la que si el numero random es menor que la probabilidad puesta, se a�ade ese cristal.
    ResourceCrystal GetDroppedCrystals()
    {
        //int RandomNumber = Random.Range(0,3);
        int RandomNumber = Random.Range(1, 101);
        
        List<ResourceCrystal> possibleCrystal = new List<ResourceCrystal>();
        // EN CRYSTALLIST
        // 0 AMARILLO
        // 1 MAGENTA
        // 2 CIAN

        if (_spawnManager.CurrentRequiredYellow == 0 && _spawnManager.CurrentRequiredCyan == 0 && _spawnManager.CurrentRequiredMagenta == 0) RecursosObligatorios = false;

        if (RecursosObligatorios)
        {
            //print("Cristal obligatorio");
            if (_spawnManager.CurrentRequiredYellow > 0 && /*RandomNumber == 0*/ RandomNumber <= _spawnManager.ProbabilityYellow /*CrystalList[0].dropChance*/)
            {
                possibleCrystal.Add(CrystalList[0]);
                _spawnManager.CurrentRequiredYellow--;
                //print("Amarillo obligatorio");
            }
            else if (_spawnManager.CurrentRequiredCyan > 0 && /*RandomNumber == 2*/ RandomNumber <= _spawnManager.ProbabilityCyan /*CrystalList[2].dropChance*/)
            {
                possibleCrystal.Add(CrystalList[2]);
                _spawnManager.CurrentRequiredCyan--;
                //print("Cian obligatorio");
            }
            else if (_spawnManager.CurrentRequiredMagenta > 0 && /*RandomNumber == 1*/ RandomNumber <= _spawnManager.ProbabilityMagenta /*CrystalList[1].dropChance*/)
            {
                possibleCrystal.Add(CrystalList[1]);
                _spawnManager.CurrentRequiredMagenta--;
                //print("Magenta obligatorio");
            }
            else
            {
                //print("No contemple probabilidad");
                if (_spawnManager.CurrentRequiredMagenta > 0)
                {
                    possibleCrystal.Add(CrystalList[1]);
                    _spawnManager.CurrentRequiredMagenta--;
                    //print("Magenta obligatorio");
                }
                else if (_spawnManager.CurrentRequiredCyan > 0)
                {
                    possibleCrystal.Add(CrystalList[2]);
                    _spawnManager.CurrentRequiredCyan--;
                    //print("Cian obligatorio");
                }
                else
                {
                    possibleCrystal.Add(CrystalList[0]);
                    _spawnManager.CurrentRequiredYellow--;
                    //print("Amarillo obligatorio");
                }

            }
            
        } 
        
        if (!RecursosObligatorios)
        {
            //print("Cristal aleatorio");

            //RANDOM DEPENDIENDO DEL DROPCHANCE DEL SPAWNAMANAGER
            int RandomNumberCrystalRandom = Random.Range(0, 3);
            int RandomProbabilityCrystalRandom = Random.Range(1, 101);
            if (RandomNumberCrystalRandom == 0 && RandomProbabilityCrystalRandom <= _spawnManager.ProbabilityYellow) possibleCrystal.Add(CrystalList[0]);
            else if (RandomNumberCrystalRandom == 1 && RandomProbabilityCrystalRandom <= _spawnManager.ProbabilityMagenta) possibleCrystal.Add(CrystalList[1]);
            else if (RandomNumberCrystalRandom == 2 && RandomProbabilityCrystalRandom <= _spawnManager.ProbabilityCyan) possibleCrystal.Add(CrystalList[2]);
            /*
            else
            {
                if (RandomNumberCrystalRandom == 0) possibleCrystal.Add(CrystalList[1]);
                else if (RandomNumberCrystalRandom == 1) possibleCrystal.Add(CrystalList[2]);
                else possibleCrystal.Add(CrystalList[0]);
            }
            */

            /*
            //RANDOM DEPENDIENDO DEL DROPCHANCE DEL SCRIPTABLE OBJECT
            foreach (ResourceCrystal item in CrystalList)
            {
                if (RandomNumber <= item.dropChance)
                {
                    possibleCrystal.Add(item);
                }
            }
            */


            /*
            //COMPLETAMENTE RANDOM
            int RandomNumberCrystalRandom = Random.Range(0, 3);
            if (RandomNumberCrystalRandom == 0) possibleCrystal.Add(CrystalList[0]);
            else if (RandomNumberCrystalRandom == 1) possibleCrystal.Add(CrystalList[1]);
            else possibleCrystal.Add(CrystalList[2]);
            */

        }

        //Esto es para que elija solo un drop para tirar entre todos
        if (possibleCrystal.Count > 0)
        {
            //print("muchos cristales, quitamos hasta que quede uno");
            ResourceCrystal droppedCrystal = possibleCrystal[Random.Range(0,possibleCrystal.Count)];
            return droppedCrystal;
        }
        else
        {
            
            //Debug.Log("No Crystal Dropped");
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

    private void Awake()
    {
        _spawnManager = GameplayManager.Instance;

        //amarillosObligatorios = _spawnManager.CurrentRequiredYellow;
        //cianesObligatorios = _spawnManager.CurrentRequiredYellow;
        //magentasObligatorios = _spawnManager.CurrentRequiredYellow;
        //probAmarillo = _spawnManager.ProbabilityYellow;
        //probCian = _spawnManager.ProbabilityCyan;
        //probMagenta = _spawnManager.ProbabilityMagenta;
    }
}
