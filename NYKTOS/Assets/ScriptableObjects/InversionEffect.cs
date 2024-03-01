using UnityEngine;
using UnityEngine.Events;

// Testeos de scriptable objects ~ Marco
[CreateAssetMenu(fileName = "InversionEffect", menuName = "InversionEffect", order = 1)]
public class InversionEffect : ScriptableObject
{
    UnityEvent<bool> inversionEvent = new UnityEvent<bool>();
    public UnityEvent<bool> InversionEvent => inversionEvent;
    
    public void Invert(bool inversionStatus) => inversionEvent.Invoke(inversionStatus);
}
