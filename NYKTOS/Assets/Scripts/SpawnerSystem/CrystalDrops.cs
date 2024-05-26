using UnityEngine;

/// <summary>
/// Contenedor global de información que se utiliza para acceder globalmente a un listado de cristales 
/// para spawnear requeridos y cuando se agoten los requerido se utiliza una probabilidad
/// </summary>
[CreateAssetMenu(fileName = "CrystalDrops", menuName = "Config/CrystalDrops")]
public class CrystalDrops : ScriptableObject
{   
    [SerializeField]
    private int _requiredYellow;
    public int RequiredYellow
    {
        get { return _requiredYellow; }
        set { _requiredYellow = value; }
    }

    [SerializeField]
    private int _requiredCyan;
    public int RequiredCyan
    {
        get { return _requiredCyan; }
        set { _requiredCyan = value; }
    }

    [SerializeField]
    private int _requiredMagenta;
    public int RequiredMagenta
    {
        get { return _requiredMagenta; }
        set { _requiredMagenta = value; }
    }

    [SerializeField]
    private int _probabilityYellow;
    public int ProbabilityYellow
    {
        get { return _probabilityYellow; }
        set { _probabilityYellow = value; }
    } 

    [SerializeField]
    private int _probabilityCyan;
    public int ProbabilityCyan
    {
        get { return _probabilityCyan; }
        set { _probabilityCyan = value; }
    }

    [SerializeField]
    private int _probabilityMagenta;
    public int ProbabilityMagenta
    {
        get { return _probabilityMagenta; }
        set { _probabilityMagenta = value; }
    }
}