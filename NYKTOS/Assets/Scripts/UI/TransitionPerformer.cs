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

    private Animator _animator;

    [SerializeField]
    private GameObject _TransitionToDay;

    [SerializeField]
    private GameObject _TransitionToNight;

    void Start()
    {
        //GameManager.Instance.GameStateChanged.AddListener(GameStateListener);
        _animator = GetComponent<Animator>();
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
    private void ResumePlayerMovement()
    {
        _playerController.enabled = true;
    }
    private void StopPlayerMovement()
    {
        _playerController.enabled = false;
    }

    private void TransitionToDark(int time)
    {
        _animator.Play("Base Layer.Crossfade_Start", 0, time);
    }

    private void TransitionToTransparent(int time)
    {
        _animator.Play("Base Layer.Crossfade_End", 0, time);
    }

    //Para debuggear
    [ContextMenu("TextDay")]
    private void TextDay()
    {
        _TransitionToDay.SetActive(true);
        _TransitionToNight.SetActive(false);
    }

    //Para debuggear
    [ContextMenu("TextNight")]
    private void TextNight()
    {
        _TransitionToDay.SetActive(false);
        _TransitionToNight.SetActive(true);
    }

    private void ResetTransition()
    {
        _animator.Play("Base Layer.Crossfade_Idle", 0, 0);
    }


    /*
    NO BORRAR HASTA QUE TODO ESTE PERFECTO BONITO
    private void TransitionPause()
    {
        Time.timeScale = 0.0f;
    }

    private void TransitionResume()
    {
        Time.timeScale = 1.0f;
    }

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
