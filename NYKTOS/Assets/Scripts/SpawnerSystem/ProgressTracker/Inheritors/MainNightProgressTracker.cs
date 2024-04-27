using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainNightProgressTracker", menuName = "Progress Trackers/Main")]
public class MainNightProgressTracker : NightProgressTracker
{
    [SerializeField]
    private CustomState _dayState;

    [SerializeField]
    private CustomState _loseSceneLoadState;

    public override void AdvanceNight()
    {
        if( _night+1 < _nightList.Count)
        {
            _night++;
            _stateMachine?.SetState(_dayState);
        }
        
        _stateMachine?.SetState(_dayState);
    }
}
