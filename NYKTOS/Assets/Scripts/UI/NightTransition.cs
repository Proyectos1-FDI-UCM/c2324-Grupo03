using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightTransition : MonoBehaviour
{
    public Animator transition;
    [SerializeField]
    private float transitionTime = 1f;

    [SerializeField]
    private GameObject _TransitionToDay;
    [SerializeField]
    private GameObject _TransitionToNight;

    private void Start()
    {
        GameManager.Instance.GameStateChanged.AddListener(GameStateListener);
        _TransitionToNight.SetActive(true);
        _TransitionToDay.SetActive(true);
    }

    private void GameStateListener(GameState state)
    {
        if (state == GameState.Night)
        {
            _TransitionToDay.SetActive(false);
            _TransitionToNight.SetActive(true);

            LoadTransition();
        }
        else if (state == GameState.Day)
        {
            _TransitionToDay.SetActive(true);
            _TransitionToNight.SetActive(false);

            LoadTransition();
        }
    }


    private void LoadTransition()
    {
        //Pone la transici�n
        transition.SetTrigger("Start");
        Invoke(nameof(ResetTransition),transitionTime);
    }
    private void ResetTransition()
    {
        transition.ResetTrigger("Start");
    }
}
