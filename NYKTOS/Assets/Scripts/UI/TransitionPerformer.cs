using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TransitionPerformer : MonoBehaviour
{
    //[SerializeField]
    //private GlobalLightcycle GlobalLightcycle;

    //[Marco] Not optimal
    [SerializeField]
    private GameStateMachine _stateMachine;

    [SerializeField]
    private InversionEffect _inversionEffect;
    private bool _Invert = false;

    [SerializeField]
    private Light2D _globalLight;

    [SerializeField]
    private PlayerController _playerController;

    void Start()
    {
        //GameManager.Instance.GameStateChanged.AddListener(GameStateListener);
    }

    //private void GameStateListener(GameState state)
    //
    //   if (state == GameState.Night)
    //   {
    //       _inversionEffect.Invert(true);
    //   }
    //   else if (state == GameState.Day)
    //   {
    //       _inversionEffect.Invert(false);
    //   }
    //



    private void ChangeIlumination()
    {
        if (_stateMachine.GetCurrentState == GlobalStateIdentifier.Night)
        {
            _inversionEffect.Invert(true);
            _globalLight.intensity = 0;
        }
        else
        {
            _inversionEffect.Invert(false);
            _globalLight.intensity = 1;
        }
    }

    private void StopPlayerMovement()
    {
        _playerController.enabled = false;
    }

    private void TransitionPause()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumePlayerMovement()
    {
        _playerController.enabled = true;
    }

    private void TransitionResume()
    {
        Time.timeScale = 1.0f;
    }

    /*
    private void TransitionUpdateState()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Day:
                GameManager.Instance.UpdateGameState(GameState.Night);
                break;
            default: 
                GameManager.Instance.UpdateGameState(GameState.Day);
                break;
        }
    }
    */


}
