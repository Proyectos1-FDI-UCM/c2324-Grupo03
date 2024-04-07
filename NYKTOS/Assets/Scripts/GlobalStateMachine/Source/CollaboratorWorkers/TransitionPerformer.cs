using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class TransitionPerformer : MonoBehaviour
{
    private Animator _animator;

    [Header("Bool emitters")]
    #region BoolEmitters
    [SerializeField]
    private BoolEmitter _DayTransitionText;
    [SerializeField]
    private BoolEmitter _NightTransitionText;
    #endregion

    [Header("Collaborator Events")]
    #region CollaboratorEvents
    [SerializeField]
    private CollaboratorEvent _TransitionToDarkEvent;
    [SerializeField]
    private CollaboratorEvent _TransitionToTransparentEvent;
    [SerializeField]
    private CollaboratorEvent _TransitionResetEvent;
    #endregion

    [Header("Texts")]
    #region Texts
    [SerializeField]
    private GameObject _TransitionToDay;
    [SerializeField]
    private GameObject _TransitionToNight;
    #endregion

    [Header("Animation Clips")]
    #region AnimationClips
    [SerializeField]
    private AnimationClip AnimationClipToDark;
    [SerializeField]
    private AnimationClip AnimationClipToTransparent;
    [SerializeField]
    private AnimationClip AnimationClipToIdle;
    #endregion

    private IEnumerator TransitionTo(AnimationClip animationClip, CollaboratorEvent collaboratorEvent)
    {
        
        Debug.Log("102");
        if (_animator != null && animationClip != null)
        {
            print("animacion lanzada");
            // Obtenemos el hash del nombre del clip de animaci�n
            int transitionHash = Animator.StringToHash(animationClip.name);
            // Reproducimos la animaci�n utilizando el hash
            _animator.Play(transitionHash, 0, 0f);

            yield return new WaitForSeconds(animationClip.length);
            Debug.Log("10:" + animationClip.length);
        }
        
        collaboratorEvent.DeleteWorker();
        yield return null;
    }

    [ContextMenu("TransitionToDark")]
    private void TransitionToDark()
    {
        _TransitionToDarkEvent.AddWorker();
        StartCoroutine(TransitionTo(AnimationClipToDark, _TransitionToDarkEvent));
    }

    [ContextMenu("TransitionToTransparent")]
    private void TransitionToTransparent()
    {
        _TransitionToTransparentEvent.AddWorker();
        StartCoroutine(TransitionTo(AnimationClipToTransparent, _TransitionToTransparentEvent));
    }

    [ContextMenu("ResetTransition")]
    private void ResetTransition()
    {
        _TransitionResetEvent.AddWorker();
        StartCoroutine(TransitionTo(AnimationClipToIdle, _TransitionResetEvent));
    }

    private void TextInDay(bool value)
    {
        _TransitionToDay.SetActive(value);
    }

    private void TextInNight(bool value)
    {
        _TransitionToNight.SetActive(value);
    }

    void Start()
    {

        _animator = GetComponent<Animator>();

        _DayTransitionText.Perform.AddListener(TextInDay);
        _NightTransitionText.Perform.AddListener(TextInNight);

        _TransitionToDarkEvent.WorkStart.AddListener(TransitionToDark);
        _TransitionToTransparentEvent.WorkStart.AddListener(TransitionToTransparent);
        _TransitionResetEvent.WorkStart.AddListener(ResetTransition);
    }

    void OnDestroy()
    {
        _DayTransitionText.Perform.RemoveListener(TextInDay);
        _NightTransitionText.Perform.RemoveListener(TextInNight);

        _TransitionToDarkEvent.WorkStart.RemoveListener(TransitionToDark);
        _TransitionToTransparentEvent.WorkStart.RemoveListener(TransitionToTransparent);
        _TransitionResetEvent.WorkStart.RemoveListener(ResetTransition);
    }
    
}
