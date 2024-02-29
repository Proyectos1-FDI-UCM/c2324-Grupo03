using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    /// <summary>
    /// Todo lo que sea un arma, es un IWeapon que tiene tanto un ataque principal como uno secundario. 
    /// Dentro de cada metodo del arma se introduce lo que se quiere hacer al atacar
    /// </summary>
    /// <param name="direction"></param>
    void PrimaryUse(Vector2 direction);

    void SecondaryUse(Vector2 direction); //En caso de solo querer un ataque, en la llamada de este metodo no se escribe nada

    /// <summary>
    /// 0 si no es nada, 1 si es daño de fuego, 2 si es paralización.
    /// Se requiere crear una variable en la que se setea a 0 en cada script de arma.
    /// </summary>
    void SetDamageType(AttackType attack);

    /// <summary>
    /// Se añade daño al arma, es decir, le suma al daño ya existente del arma el número entero que se le pasa.
    /// </summary>
    //void AddWeaponDamage(int num);

}
