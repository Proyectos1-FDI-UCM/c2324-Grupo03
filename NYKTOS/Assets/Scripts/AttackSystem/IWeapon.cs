using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Todo lo que sea un arma debe heredar de esta clase abstracta y hacer override a los metodos que se proporcionan. Todo arma tiene ataque primario y secundario.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// Ataque primario del arma.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="_damage"></param>
    /// <param name="attackType"></param>
    public abstract void PrimaryUse(Vector2 direction, int _damage, AttackType attackType);

    /// <summary>
    /// Ataque secundario del arma.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="_damage"></param>
    /// <param name="attackType"></param>
    public abstract void SecondaryUse(Vector2 direction, int _damage, AttackType attackType);

    //Eventos que surgen al atacar con el arma. Esto es para que se puedan implementar sonidos.
    [SerializeField]
    public UnityEvent _primaryUsePerformed;

    [SerializeField]
    public UnityEvent _secondaryUsePerformed;


}


