using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "GameProgressTracker", menuName = "Manager/GameProgress")]
public class NightProgressTracker : ScriptableObject
{
    [SerializeField]
    private FloatEmitter _nightTimeEmitter;

    [SerializeField]
    private GameStateMachine _stateMachine;

    [SerializeField]
    private int _night = 0;
    public int Night 
    { 
        get { return _night; } 
        set { _night = value; }
    }

    [SerializeField]
    private List<NightWave> _nightList = new List<NightWave>();

    private UnityEvent<NightWave> _startNight = new UnityEvent<NightWave> ();
    public UnityEvent<NightWave>  StartNight => _startNight;

    public void InvokeStartNight()
    {
        _startNight.Invoke(_nightList[_night]);
        _nightTimeEmitter.Perform.Invoke((float)_nightList[_night].NightLength);
    }

    public void AdvanceNight()
    {   
        Debug.Log(_stateMachine.GetCurrentState);
        if(_stateMachine.GetCurrentState == GlobalStateIdentifier.Night)
        {
            if( _night+1 < _nightList.Count)
            {
                _night++;
                _stateMachine?.SetState(GlobalStateIdentifier.Day);
            }
            else
            {
                _stateMachine?.SetState(GlobalStateIdentifier.Lose);
            }
        }
        else if(_stateMachine.GetCurrentState == GlobalStateIdentifier.TutorialNight)
        {
            _stateMachine?.SetState(GlobalStateIdentifier.MaingameSceneLoad);
        }
    }

    public void ResetNights()
    {
        _night = 0;
    }
}