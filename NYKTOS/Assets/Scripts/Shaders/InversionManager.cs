using UnityEngine;

public class InversionManager : MonoBehaviour
{

    public delegate void LightSwapDelegate();
    private event LightSwapDelegate sEvent;

    public LightSwapDelegate swapEvent
    {
        get { return sEvent; }
        set { sEvent = value; }
    }

    static private InversionManager _instance; 
    static public InversionManager Instance 
    { 
        get { return _instance; } 
    }

    [SerializeField]
    private bool swapStatus = false;
    public bool getSwap
    {
        get { return swapStatus; }
    } 

    public void SetSwap(bool state){
        swapStatus = state;
        if (swapEvent != null)
        {
            swapEvent();
        }
    }

    // Metodo para debuggear
    [ContextMenu("ForceSwap")]
    public void ForceSwap()
    {
        swapStatus = swapStatus ? false : true;

        if (swapEvent != null)
        {
            swapEvent();
        }
    }

    private void Awake()
    {
        if (_instance != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}