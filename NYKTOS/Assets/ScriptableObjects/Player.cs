using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ScriptableObject
{
    public int life;

    [SerializeField]
    public enum weapon
    {
        Palo = 0, Cetro = 1
    }

    public int numYcrystal;

    public int numMcrystal;

    public int numCcrystal;

}
