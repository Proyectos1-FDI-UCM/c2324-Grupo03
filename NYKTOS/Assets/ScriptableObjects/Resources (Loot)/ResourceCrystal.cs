using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crystal", menuName = "Crystal")]
public class ResourceCrystal : ScriptableObject
{
    public Sprite CristalSprite;
    public string CristalName;
    public int dropChance;
    public int dissapearTime;

    public ResourceCrystal(string CristalName, int dropChance)
    {
        this.CristalName = CristalName;
        this.dropChance = dropChance;
    }
}
