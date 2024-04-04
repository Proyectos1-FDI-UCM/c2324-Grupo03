using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "GameProgressTracker", menuName = "Manager/GameProgress")]
public class NightProgressTracker : ScriptableObject
{   
    [SerializeField]
    private GameStateMachine _stateMachine;

    [SerializeField]
    private int _night = 0;

    [SerializeField]
    private List<NightWave> _nightList = new List<NightWave>();

    private UnityEvent<NightWave> _startNight = new UnityEvent<NightWave> ();
    public UnityEvent<NightWave>  StartNight => _startNight;

    public void InvokeStartNight()
    {
        _startNight.Invoke(_nightList[_night]);
    }

    public void AdvanceNight()
    {   
        if(_stateMachine.GetCurrentState == GlobalStateIdentifier.Night)
        {
            if( _night+1 < _nightList.Count)
            {
                _night++;
            }
            _stateMachine?.SetState(GlobalStateIdentifier.Day);
        }
    }

}