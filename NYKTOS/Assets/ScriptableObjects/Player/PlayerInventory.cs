using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "PlayerInventory", order = 1)]
public class PlayerInventory : ScriptableObject
{
    public int yellowCrystals = 0;
    
    public int magentaCrystals = 0;

    public int cyanCrystals = 0;

    [ContextMenu("Reset")]
    public void Reset(){
        yellowCrystals = 0;
        magentaCrystals = 0;
        cyanCrystals = 0;
    }

}
