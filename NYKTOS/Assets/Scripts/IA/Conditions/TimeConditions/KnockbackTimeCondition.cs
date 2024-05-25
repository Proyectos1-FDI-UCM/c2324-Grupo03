using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que controla la frecuencia con la que se puede hacer retroceder a las entidades del juego (Cooldown del Knockback)
/// </summary>


public class KnockbackTimeCondition : MonoBehaviour, ICondition
{
    private RBMovement _rbMovement;
    private float currentTime = 0f;
    public bool Validate(GameObject _object) //Comprueba que haya pasado suficiente tiempo desde el último knockback
    {
        
        if (currentTime < _rbMovement.knockBackTime)
        {
            currentTime += Time.deltaTime;
            return false;
        }
        else
        {
            currentTime = 0;
            return true;
        }
    }

    private void Awake()
    {
        _rbMovement = GetComponentInParent<RBMovement>();
    }
}
