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
    private List<GameObject> _TransitionTexts;
    //TransitionTexts[0] TextoDeTransicionParaNoche
    //TransitionTexts[1] TextoDeTransicionParaDia

    private GameObject _TransitionToDay;
    private GameObject _TransitionToNight;

    private void Start()
    {
        GameManager.Instance.GameStateChanged.AddListener(GameStateListener);
        _TransitionToNight = _TransitionTexts[0].gameObject;
        _TransitionToDay = _TransitionTexts[1].gameObject;
        _TransitionToNight.SetActive(true);
        _TransitionToDay.SetActive(true);
    }

    private void GameStateListener(GameState state)
    {
        if (state == GameState.Night)
        {
            _TransitionToDay.SetActive(false);
            _TransitionToNight.SetActive(true);
        }
        else if (state == GameState.Day)
        {
            _TransitionToDay.SetActive(true);
            _TransitionToNight.SetActive(false);
        }
    }


    public void LoadTransition()
    {
        //Pone la transición
        transition.SetTrigger("Start");
        GameStateListener(GameManager.Instance.State);
    }

    public void LoadLevel()
    {
        //Acaba la transición y empieza la noche o el día
        transition.ResetTrigger("Start");
    }


}
