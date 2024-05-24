using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


//Codigo de Iker
public class CrystalBag : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory inventory;
    public GameObject droppedItemPrefab;
    private ResourceCrystal _droppedCrystal;
    public ResourceCrystal droppedCrystal
    {
        get { return _droppedCrystal; }
    }
    private GameObject crystalGameObject;

    /// <summary>
    /// En la lista de Cristales ponemos todos los cristales creados con el ScriptableObject CrystalResources (Hay solo 3)
    /// En la lista de CristalesClonados ponemos los cristales que se van a instanciar en escena
    /// </summary>
    public List<ResourceCrystal> CrystalList = new List<ResourceCrystal>();
    private List<GameObject> clonedCrystals = new List<GameObject>();

    /// <summary>
    /// Componente para manejar las probabilidades de drop
    /// </summary>
    [SerializeField]
    private CrystalDrops _crystalDropsTracker;

    private bool RecursosObligatorios = true;

    /// <summary>
    /// Esto usa la logica del porcentaje, toma un numero aleatorio entre 1 y 100.
    /// Se recopila la lista de cristales con su probabilidad que puede soltar el enemigo.
    /// Dependiendo del tipo de enemigo podemos poner los cristales que nosotros queramos que dropee
    /// Entonces para cada Crystal de la lootList toma una condici�n en la que si el numero random es menor que la probabilidad puesta, se a�ade ese cristal. 
    /// </summary>
    ResourceCrystal GetDroppedCrystals()
    {
        int RandomNumber = Random.Range(1, 101);
        
        List<ResourceCrystal> possibleCrystal = new List<ResourceCrystal>();
        // EN CRYSTALLIST
        // 0 : CRISTAL AMARILLO
        // 1 : CRISTAL MAGENTA
        // 2 : CRISTAL CIAN

        //Hasta que no se acaben los cristales obligatorios a soltar, se mantendrá la condición de que suelte primero los obligatorios para luego dar paso a los aleatorios.
        if (_crystalDropsTracker.RequiredYellow == 0 && _crystalDropsTracker.RequiredCyan == 0 && _crystalDropsTracker.RequiredMagenta == 0) RecursosObligatorios = false;

        //En los recursos obligatorios se decide si la probabilidad del RandomNumber es menor que la probabilidad del cristal seleccionado, aparte de que todavía queden cristales por soltar.
        if (RecursosObligatorios)
        {
            if (_crystalDropsTracker.RequiredYellow > 0 && RandomNumber <= _crystalDropsTracker.ProbabilityYellow)
            {
                possibleCrystal.Add(CrystalList[0]);
                _crystalDropsTracker.RequiredYellow--;
            }
            else if (_crystalDropsTracker.RequiredCyan > 0 && RandomNumber <= _crystalDropsTracker.ProbabilityCyan)
            {
                possibleCrystal.Add(CrystalList[2]);
                _crystalDropsTracker.RequiredCyan--;
            }
            else if (_crystalDropsTracker.RequiredMagenta > 0 && RandomNumber <= _crystalDropsTracker.ProbabilityMagenta)
            {
                possibleCrystal.Add(CrystalList[1]);
                _crystalDropsTracker.RequiredMagenta--;
            }
            else
            {
                //En caso de que la probabilidad siempre sea mas baja que los cristales a droppear se seleccionara un posible cristal de los que quedan obligatorios
                if (_crystalDropsTracker.RequiredMagenta > 0)
                {
                    possibleCrystal.Add(CrystalList[1]);
                    _crystalDropsTracker.RequiredMagenta--;
                }
                else if (_crystalDropsTracker.RequiredCyan > 0)
                {
                    possibleCrystal.Add(CrystalList[2]);
                    _crystalDropsTracker.RequiredCyan--;
                }
                else
                {
                    possibleCrystal.Add(CrystalList[0]);
                    _crystalDropsTracker.RequiredYellow--;
                }

            }
        } 
        
        //Cuando se terminen los recursos obligatorios se pasara a una probabilidad que dependera del DropsTrackerProbability
        //Si la probabilidad no cumple ninguna condición no se soltará ningún cristal
        if (!RecursosObligatorios)
        {
            int RandomNumberCrystalRandom = Random.Range(0, 3);
            int RandomProbabilityCrystalRandom = Random.Range(1, 101);
            if (RandomNumberCrystalRandom == 0 && RandomProbabilityCrystalRandom <= _crystalDropsTracker.ProbabilityYellow) possibleCrystal.Add(CrystalList[0]);
            else if (RandomNumberCrystalRandom == 1 && RandomProbabilityCrystalRandom <= _crystalDropsTracker.ProbabilityMagenta) possibleCrystal.Add(CrystalList[1]);
            else if (RandomNumberCrystalRandom == 2 && RandomProbabilityCrystalRandom <= _crystalDropsTracker.ProbabilityCyan) possibleCrystal.Add(CrystalList[2]);
            
            //EN CASO DE QUE QUERAMOS QUE SIEMPRE SUELTE UN CRISTAL SIN CONTEMPLAR LA PROBABILIDAD DE QUE NO SUELTE UN CRISTAL
            /*
            else
            {
                if (RandomNumberCrystalRandom == 0) possibleCrystal.Add(CrystalList[0]);
                else if (RandomNumberCrystalRandom == 1) possibleCrystal.Add(CrystalList[1]);
                else possibleCrystal.Add(CrystalList[2]);
            }
            */

            //RANDOM DEPENDIENDO DEL DROPCHANCE DEL SCRIPTABLE OBJECT
            /*
            foreach (ResourceCrystal item in CrystalList)
            {
                if (RandomNumber <= item.dropChance)
                {
                    possibleCrystal.Add(item);
                }
            }
            */

            //COMPLETAMENTE RANDOM
            /*
            int RandomNumberCrystalRandom = Random.Range(0, 3);
            if (RandomNumberCrystalRandom == 0) possibleCrystal.Add(CrystalList[0]);
            else if (RandomNumberCrystalRandom == 1) possibleCrystal.Add(CrystalList[1]);
            else possibleCrystal.Add(CrystalList[2]);
            */

        }

        //Si hay varios cristales, se elige uno entre ellos para ser soltado
        if (possibleCrystal.Count > 0)
        {
            ResourceCrystal droppedCrystal = possibleCrystal[Random.Range(0,possibleCrystal.Count)];
            return droppedCrystal;
        }
        //No hay cristal para soltar en caso de no haber cristal seleccionado
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Llama al cristal seleccionado
    /// Instancia el cristal
    /// Obtiene su spriterenderer para diferenciar que cristal es
    /// Lo añade a la lista de cristales clonados
    /// Se activa después de eso un contador para que desaparezca después de un tiempo dado
    /// </summary>
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

    //Para poder interactuar con los cristales necesitamos obtener su componente que es este mismo script, se lo aplicamos a cada cristal que se genere
    public void InteractWithCrystalClones()
    {
        foreach (GameObject clone in clonedCrystals)
        {
            GetComponent<CrystalBag>();
        }
    }
}
