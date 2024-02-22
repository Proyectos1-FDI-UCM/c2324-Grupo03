using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    //Todo lo que sea un arma, es un IWeapon que tiene tanto un ataque principal como uno secundario. Dentro de cada metodo del arma se introduce lo que se quiere hacer al atacar
    void PrimaryUse(Vector2 direction);
    void SecondaryUse(Vector2 direction); //En caso de solo querer un ataque, en la llamada de este metodo no se escribe nada
}
