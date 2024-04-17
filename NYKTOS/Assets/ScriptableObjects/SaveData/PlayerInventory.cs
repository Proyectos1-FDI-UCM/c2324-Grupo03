
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

    [SerializeField]
    private ScriptableObject _currentWeapon;

    private UnityEvent inventoryUpdateEvent = new UnityEvent();
    public UnityEvent InventoryUpdate => inventoryUpdateEvent;

    private void InvokeInventoryUpdate() => inventoryUpdateEvent.Invoke();

    [ContextMenu("Reset")]
    public void Reset(){
        amarilloCristales = 6;
        magentaCristales = 0;
        cianCristales = 0;
        InvokeInventoryUpdate();
    }

    [ContextMenu("TutorialInventoryAdjust")]
    public void TutorialInventoryAdjust()
    {
        amarilloCristales = 3;
        magentaCristales = 0;
        cianCristales = 0;
        InvokeInventoryUpdate();
    }

    [ContextMenu("ResetForTutorial")]
    public void ResetForTutorial(){
        amarilloCristales = 6;
        magentaCristales = 0;
        cianCristales = 0;
        InvokeInventoryUpdate();
    }

    public void OnValidate()
    {
        InvokeInventoryUpdate();
    }

}
