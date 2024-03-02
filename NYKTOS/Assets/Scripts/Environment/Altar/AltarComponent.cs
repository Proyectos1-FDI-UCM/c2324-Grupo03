using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarComponent : MonoBehaviour, IInteractable
{
    // Codigo de Iker y Andrea :D

    //VA ACTUALIZANDO EL ESTADO EN EL QUE SE ENCUENTRA EL ALTAR
    //PORCENTAJE DE VIDA EN INT
    //CONSTRUCCION AUTOMATICA CON UN BOOLEANO
    //SI ESTA CONSTRUIDO, ATRAE ENEMIGOS, ACTIVA VIDA Y NO SE PUEDE INTERACTUAR CON EL HASTA QUE SEA DESTRUIDO (ALTARHEALTHCOMPONENT)
    //SI ESTA DESTRUIDO, YA NO ATRAE ENEMIGOS, DESACTIVA EL FACTOR VIDA SE PUEDE INTERACTUAR CON EL (ALTARDESTROYCOMPONENT)

    #region references
    private Transform _myTransform;
    #endregion

    public void Interact()
    {
        Debug.Log("hola soy un altar");
    }

    public void QuitInteraction() { }

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
