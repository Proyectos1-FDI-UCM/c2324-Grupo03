using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Explicaci�n te�rica de la interfaz hecha por Iker :D

//Lo mismo que en IPlayerAttributes pero en vez de usarse en PlayerController se hace en el AltarComponent
public interface IAltarAttributes
{
    Vector3 GetAltarPosition();
}
