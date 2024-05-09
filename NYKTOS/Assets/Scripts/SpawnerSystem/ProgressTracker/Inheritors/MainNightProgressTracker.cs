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
        Debug.Log($"[MAIN NIGHT PROGRESS TRACKER] Advance night - This night: {_night} - Night count: {_nightList.Count}");
        if( _night + 1 < _nightList.Count)
        {
            _night++;

            Debug.Log($"[MAIN NIGHT PROGRESS TRACKER] night+1 < nightList.Count IS TRUE - This night: {_night} - Night count: {_nightList.Count}");
        }
        
        _stateMachine?.SetState(_dayState);
    }
}
