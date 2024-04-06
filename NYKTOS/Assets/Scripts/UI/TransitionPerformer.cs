using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TransitionPerformer : MonoBehaviour
{
    private Animator _animator;

    #region BoolEmitters
    [SerializeField]
    private BoolEmitter _DayTransitionText;
    [SerializeField]
    private BoolEmitter _NightTransitionText;
    #endregion

    #region VoidEmmiters
    [SerializeField]
    private VoidEmitter _TransitionToDark;
    [SerializeField]
    private VoidEmitter _TransitionToTransparent;
    [SerializeField]
    private VoidEmitter _TransitionReset;
    #endregion

    #region Texts
    [SerializeField]
    private GameObject _TransitionToDay;
    [SerializeField]
    private GameObject _TransitionToNight;
    #endregion

    #region AnimationClips
    [SerializeField]
    private AnimationClip AnimationClipToDark;
    [SerializeField]
    private AnimationClip AnimationClipToTransparent;
    [SerializeField]
    private AnimationClip AnimationClipToIdle;
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();

        _DayTransitionText.Perform.AddListener(TextInDay);
        _NightTransitionText.Perform.AddListener(TextInNight);

        _TransitionToDark.Perform.AddListener(TransitionToDark);
        _TransitionToTransparent.Perform.AddListener(TransitionToTransparent);
        _TransitionReset.Perform.AddListener(ResetTransition);

    }

   
    [ContextMenu("TransitionToDark")]
    private void TransitionToDark()
    {
        if (_animator != null && AnimationClipToDark != null)
        {
            print("animacion lanzada");
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
    private void TextInDay(bool value)
    {
        _TransitionToDay.SetActive(value);
    }

    private void TextInNight(bool value)
    {
        _TransitionToNight.SetActive(value);
    }
    
}
