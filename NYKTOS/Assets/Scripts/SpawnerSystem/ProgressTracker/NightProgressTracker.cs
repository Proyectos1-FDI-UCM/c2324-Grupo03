using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class NightProgressTracker : ScriptableObject
{
    [SerializeField]
    private FloatEmitter _nightTimeEmitter;

    [SerializeField]
    protected GameStateMachine _stateMachine;

    [SerializeField]
    protected int _night = 0;
    public int Night 
    { 
        get { return _night; } 
        set { _night = value; }
    }

    [SerializeField]
    protected List<NightWave> _nightList = new List<NightWave>();

    private UnityEvent<NightWave> _startNight = new UnityEvent<NightWave> ();
    public UnityEvent<NightWave>  StartNight => _startNight;

    public void InvokeStartNight()
    {
        Debug.LogError("[NIGHT PROGRESS TRACKER] Inicializada noche " + _night);
        _startNight.Invoke(_nightList[_night]);
        _nightTimeEmitter.Perform.Invoke(_nightList[_night].NightLength);
    }

    public abstract void AdvanceNight();

    public void ResetNights()
    {
        _night = 0;
    }
}