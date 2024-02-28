using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nota de Iker, esto es una Interfaz
//Antes había cuatro interfaces para pillar la posicion de un objeto en especifico, en este caso hice este script como simplificacion de las 4
//Se puede usar para mas propositos si se desea

//Explicación teórica de la interfaz hecha por Iker :D

//Una interfaz es un "contrato", a la hora de utilizarlo en el PlayerController, ese script debe asegurarse de devolver
//un valor (en este caso Vector3) a traves de un metodo con el mismo nombre y de privacidad publica, para aplicar esa condición se pone IPlayerAttributes
//al lado del MonoBehaviour, entonces al hacer el metodo GetPlayerPosition en PlayerController, tendremos el Vector3 de manera global.

//Dos pasos para obtener ese Vector3 globalizado en el enemigo y utilizarlo
//1. Declarar un private IPlayerAttributes playerAttributes en el enemigo para que tome los datos de la interfaz
//2. En el start poner playerAttributes = FindObjectOfType<PlayerController>(); para que tome la información de la interfaz en el script que devuelve el Vector3

public interface IPositionAttributes
{
    Vector3 GetPosition();
}
