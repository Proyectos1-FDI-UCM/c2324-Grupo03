using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Defensetype { beacon, wall, turret };

[CreateAssetMenu(fileName = "New Defense", menuName = "Defense")]
public class Defense : ScriptableObject
{
    [SerializeField]
    private Defensetype _defenseType;
    [SerializeField]
    private int _health;
    public int health { get { return _health; } }
    public bool built;

    //ASHER AQUI PON COSITAS DE UI :D

}
