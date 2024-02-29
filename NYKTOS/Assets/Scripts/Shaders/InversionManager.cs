using UnityEngine;

public class InversionManager : MonoBehaviour
{

    public delegate void MiEventoDelegate();
    private event MiEventoDelegate MiEvento;

    public MiEventoDelegate evento
    {
        get { return MiEvento; }
        set { MiEvento = value; }
    }

    static private InversionManager _instance; 
    static public InversionManager Instance 
    { 
        get { return _instance; } 
    }

    [SerializeField]
    private bool swap = false;
    public bool getSwap
    {
        get { return swap; }
    } 

    public void SetSwap(bool state){
        swap = state;
        if (MiEvento != null)
        {
            MiEvento();
        }
    }

    // Metodo para debuggear
    [ContextMenu("Swap")]
    public void Swap()
    {
        swap = swap ? false : true;

        if (MiEvento != null)
        {
            MiEvento();
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