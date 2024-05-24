using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crystal", menuName = "Crystal")]

//Scriptable object hecho por Iker
public class ResourceCrystal : ScriptableObject
{
    /// <summary>
    /// Sprite del cristal creado
    /// </summary>
    public Sprite CristalSprite;
    /// <summary>
    /// Nombre del cristal creado
    /// </summary>
    public string CristalName;
    /// <summary>
    /// Probabilidad de ser generado el cristal
    /// </summary>
    public int dropChance;
    /// <summary>
    /// Tiempo de desaparición del cristal
    /// </summary>
    public int dissapearTime;

    /// <summary>
    /// Obtención de características del cristal, asi como su nombre o su probabilidad de drop
    /// </summary>
    /// <param name="CristalName"></param>
    /// <param name="dropChance"></param>
    public ResourceCrystal(string CristalName, int dropChance)
    {
        this.CristalName = CristalName;
        this.dropChance = dropChance;
    }
}
