using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialNightProgressTracker", menuName = "Progress Trackers/Tutorial")]
public class TutorialNightProgressTacker : NightProgressTracker
{
    [SerializeField]
    private CustomState _maingameSceneLoadState;

    public override void AdvanceNight()
    {
        _stateMachine?.SetState(_maingameSceneLoadState);
    }
}
