using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TransitionPerformer : MonoBehaviour
{
    private Animator _animator;

    /*
    [SerializeField]
    private BoolEmitter _DayTransition;

    [SerializeField]
    private BoolEmitter _NightTransition;
    */

    [SerializeField]
    private GameObject _TransitionToDay;

    [SerializeField]
    private GameObject _TransitionToNight;

    [SerializeField]
    private AnimationClip AnimationClipToDark;

    [SerializeField]
    private AnimationClip AnimationClipToTransparent;

    [SerializeField]
    private AnimationClip AnimationClipToIdle;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _TransitionToDay.SetActive(false);
        _TransitionToNight.SetActive(false);
        //_DayTransition.Perform.AddListener(TextInDay);
        //_NightTransition.Perform.AddListener(TextInNight);
    }

   
    [ContextMenu("TransitionToDark")]
    private void TransitionToDark()
    {
        if (_animator != null && AnimationClipToDark != null)
        {
            // Obtenemos el hash del nombre del clip de animación
            int transitionHash = Animator.StringToHash(AnimationClipToDark.name);
            // Reproducimos la animación utilizando el hash
            _animator.Play(transitionHash, 0, 0f);
        }
    }

    [ContextMenu("TransitionToTransparent")]
    private void TransitionToTransparent()
    {
        if (_animator != null && AnimationClipToTransparent != null)
        {
            // Obtenemos el hash del nombre del clip de animación
            int transitionHash = Animator.StringToHash(AnimationClipToTransparent.name);
            // Reproducimos la animación utilizando el hash
            _animator.Play(transitionHash, 0, 0f);
        }
    }

    [ContextMenu("ResetTransition")]
    private void ResetTransition()
    {
        if (_animator != null && AnimationClipToIdle != null)
        {
            // Obtenemos el hash del nombre del clip de animación
            int transitionHash = Animator.StringToHash(AnimationClipToIdle.name);
            // Reproducimos la animación utilizando el hash
            _animator.Play(transitionHash, 0, 0f);
        }
    }

    //Para debuggear
    private void TextInDay(bool value)
    {
        _TransitionToDay.SetActive(value);
    }

    private void TextInNight(bool value)
    {
        _TransitionToNight.SetActive(value);
    }
    
}
