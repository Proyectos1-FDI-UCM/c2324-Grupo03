using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEditor;
using UnityEngine;

public class CrystalBag : MonoBehaviour
{
    private float dropForce = 20f;
    private bool ObtainedCrystal = false;
    //public Collider2D _playerCollider;
    //Necesitamos el CrystalPrefab aquí
    public GameObject droppedItemPrefab;
    private GameObject crystalGameObject;
    [SerializeField]
    private GameObject _player;
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
        ResourceCrystal droppedCrystal = GetDroppedCrystals();
        if (droppedCrystal != null)
        {
            crystalGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            crystalGameObject.GetComponent<SpriteRenderer>().sprite = droppedCrystal.CristalSprite;

            /*
            Vector2 PlayerPosition = player.transform.position;
            Vector2 dropDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
            crystalGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            */

            int DissapearTime = droppedCrystal.dissapearTime;
            Destroy(crystalGameObject,DissapearTime);

            if (ObtainedCrystal)
            {
                Destroy(crystalGameObject);
            }
        }
    }

    private void Update()
    {
       if (crystalGameObject != null)
        {
            Vector2 DirectionToPlayer = _player.transform.position - transform.position;
            crystalGameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(DirectionToPlayer * dropForce, _player.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       //if (_playerCollider != null) ObtainedCrystal = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (_playerCollider == null) ObtainedCrystal = false;
    }


}
