using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Contenedor para registrar el progreso del jugador como la siguiente
/// noche. Además es el intermediario entre eventos creados por estados de
/// juego y nightmanager
/// </summary>
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

    /// <summary>
    /// Lanza los eventos de inicio de noche
    /// </summary>
    public void InvokeStartNight()
    {
        Debug.Log($"[NIGHT PROGRESS TRACKER] Inicializada noche {_night} - {_nightList[_night].NightLength}");
        _startNight.Invoke(_nightList[_night]);
        _nightTimeEmitter.InvokePerform(_nightList[_night].NightLength);
    }

    /// <summary>
    /// Gestiona como modificar los datos de progreso al finalzar la noche.
    /// 
    /// Es abstracta ya que hay diferentes tipos de formas de registrar el progreso
    /// </summary>
    public abstract void AdvanceNight();

    public void ResetNights()
    {
        _night = 0;
    }
}