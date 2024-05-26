using UnityEngine;

/// <summary>
/// Tracker para el juego principal (es decir, lo que no es el tutorial). 
/// 
/// <para>
/// Si al terminar una noche quedan mas a las que avanzar se avanza y se 
/// pasa al día, en caso de que no queden mas noches no se avanza la noche 
/// y se pasa al día también.
/// </para>
/// 
/// <para>
/// Esto significa que una vez se llegue a la última noche disponible esta
/// se repetirá indefinidamente hasta que se gane o pierda
/// </para>
/// 
/// </summary>
[CreateAssetMenu(fileName = "MainNightProgressTracker", menuName = "Progress Trackers/Main")]
public class MainNightProgressTracker : NightProgressTracker
{
    [SerializeField]
    private CustomState _dayState;

    [SerializeField]
    private CustomState _loseSceneLoadState;

    public override void AdvanceNight()
    {
        Debug.Log($"[MAIN NIGHT PROGRESS TRACKER] Advance night - This night: {_night} - Night count: {_nightList.Count}");
        if( _night + 1 < _nightList.Count)
        {
            _night++;

            Debug.Log($"[MAIN NIGHT PROGRESS TRACKER] night+1 < nightList.Count IS TRUE - This night: {_night} - Night count: {_nightList.Count}");
        }
        
        _stateMachine?.SetState(_dayState);
    }
}
