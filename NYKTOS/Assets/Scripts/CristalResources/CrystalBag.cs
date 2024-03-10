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
    
    //Esto usa la logica del porcentaje, toma un numero aleatorio entre 1 y 100.
    //Se recopila la lista de cristales con su probabilidad que puede soltar el enemigo.
    //Dependiendo del tipo de enemigo podemos poner los cristales que nosotros queramos que dropee //FALTA HACER CAMBIO DE PROBABILIDAD
    //Entonces para cada Crystal de la lootList toma una condición en la que si el numero random es menor que la probabilidad puesta, se añade ese cristal.
    ResourceCrystal GetDroppedCrystals()
    {
        
        int RandomNumber = Random.Range(1, 101);
        List<ResourceCrystal> possibleCrystal = new List<ResourceCrystal>();
        foreach (ResourceCrystal item in CrystalList)
        {
            if (RandomNumber <= item.dropChance)
            {
                possibleCrystal.Add(item);
            }
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

            int DissapearTime = _droppedCrystal.dissapearTime;
            Destroy(crystalGameObject,DissapearTime);
        }
    }

}
