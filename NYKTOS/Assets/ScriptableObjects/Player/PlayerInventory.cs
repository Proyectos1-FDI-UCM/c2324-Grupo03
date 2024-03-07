using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "PlayerInventory", order = 1)]
public class PlayerInventory : ScriptableObject
{
    [SerializeField]
    private int amarilloCristales = 10;
    public int Amarillo
    {
        get
        {
            return amarilloCristales;
        }
        set
        {
            amarilloCristales = value;
            InvokeInventoryUpdate();
        }
    }
    
    [SerializeField]
    private int magentaCristales = 0;
    public int Magenta
    {
        get
        {
            return magentaCristales;
        }
        set
        {
            magentaCristales = value;
            InvokeInventoryUpdate();
        }
    }
    
    [SerializeField]
    private int cianCristales = 0;
    public int Cian
    {
        get
        {
            return cianCristales;
        }
        set
        {
            cianCristales = value;
            InvokeInventoryUpdate();
        }
    }

    private UnityEvent inventoryUpdateEvent = new UnityEvent();
    public UnityEvent InventoryUpdate => inventoryUpdateEvent;

    private void InvokeInventoryUpdate() => inventoryUpdateEvent.Invoke();

    [ContextMenu("Reset")]
    public void Reset(){
        amarilloCristales = 0;
        magentaCristales = 0;
        cianCristales = 0;
    }

    public void OnValidate()
    {
        InvokeInventoryUpdate();
    }

    public void Awake()
    {
        amarilloCristales = 10;
        cianCristales = 0;
        magentaCristales=0;
    }
}
