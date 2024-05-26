using UnityEngine;

/// <summary>
/// Tracker para niveles de tutorial. Al avanzar la noche se ha acabado el tutorial
/// por lo que se pasa el estado de carga de la escena principal
/// </summary>
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
