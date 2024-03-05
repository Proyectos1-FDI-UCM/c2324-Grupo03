using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script de Iker y Marco
/// </summary>
[CreateAssetMenu(fileName = "InversionEffect", menuName = "InversionEffect", order = 1)]
public class InversionEffect : ScriptableObject
{
    [SerializeField]
    private bool _inversionStatus;
    public bool InversionStatus
    {
        get
        {
            return _inversionStatus;
        }
        set
        {
            _inversionStatus = value;
            Invert(_inversionStatus);
        }
    }

    private UnityEvent<bool> inversionEvent = new UnityEvent<bool>();
    public UnityEvent<bool> InversionEvent => inversionEvent;
    
    /// <summary>
    /// Lanza el evento inversionEvent
    /// </summary>
    /// <param name="inversionStatus"></param>
    public void Invert(bool inversionStatus) => inversionEvent.Invoke(inversionStatus);

    public void OnValidate()
    {
        Invert(_inversionStatus);
    }
}