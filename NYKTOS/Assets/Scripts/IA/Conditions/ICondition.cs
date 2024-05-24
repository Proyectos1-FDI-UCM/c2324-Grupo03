using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICondition
{
    /// <summary>
    /// Todo lo que sea una condicion tiene este metodo. Retorna un bool.
    /// </summary>
    public bool Validate(GameObject _object);
}
