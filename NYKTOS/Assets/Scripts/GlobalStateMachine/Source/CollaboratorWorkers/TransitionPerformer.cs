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
    private BoolEmitter _dayTransitionTextEmitter;
    [SerializeField]
    private BoolEmitter _nightTransitionTextEmitter;
    #endregion

    [Header("Collaborator Events")]
    #region CollaboratorEvents
    [SerializeField]
    private CollaboratorEvent _transitionToDarkEvent;
    [SerializeField]
    private CollaboratorEvent _transitionToTransparentEvent;
    [SerializeField]
    private CollaboratorEvent _transitionResetEvent;
    #endregion

    [Header("Texts")]
    #region Texts
    [SerializeField]
    private GameObject _transitionToDayText;
    [SerializeField]
    private GameObject _transitionToNightText;
    #endregion

    [Header("Animation Clips")]
    #region AnimationClips
    [SerializeField]
    private AnimationClip _animationClipToDark;
    private int _animationHashClipToDark;
    [SerializeField]
    private AnimationClip _animationClipToTransparent;
    private int _animationHashClipToTransparent;
    [SerializeField]
    private AnimationClip _animationClipToIdle;
    private int _animationHashClipToIdle;
    #endregion

    private IEnumerator TransitionTo
    (
        int animationHash, 
        float animationDuration, 
        CollaboratorEvent collaboratorEvent
    )
    {
        if (_animator != null)
        {
            _animator.Play(animationHash);

            yield return new WaitForSeconds(animationDuration);
        }
        
        collaboratorEvent.DeleteWorker();
        yield return null;
    }

    [ContextMenu("TransitionToDark")]
    private void TransitionToDark()
    {
        StopAllCoroutines();
        
        _transitionToDarkEvent.AddWorker();
        
        StartCoroutine
        (
            TransitionTo
            (
                _animationHashClipToDark, 
                _animationClipToDark.length, 
                _transitionToDarkEvent
            )
        );
    }

    [ContextMenu("TransitionToTransparent")]
    private void TransitionToTransparent()
    {
        Debug.Log("TRANSICION");
        StopAllCoroutines();
        
        _transitionToTransparentEvent.AddWorker();
        
        StartCoroutine
        (
            TransitionTo
            (
                _animationHashClipToTransparent, 
                _animationClipToTransparent.length, 
                _transitionToTransparentEvent
            )
        );
    }

    [ContextMenu("ResetTransition")]
    private void ResetTransition()
    {
        StopAllCoroutines();
        
        _transitionResetEvent.AddWorker();
        
        StartCoroutine
        (
            TransitionTo
            (
                _animationHashClipToIdle, 
                _animationClipToIdle.length,
                _transitionResetEvent
            )
        );
    }

    private void TextInDay(bool value)
    {
        _transitionToDayText.SetActive(value);
    }

    private void TextInNight(bool value)
    {
        _transitionToNightText.SetActive(value);
    }

    void Start()
    {

        _animator = GetComponent<Animator>();

        _animationHashClipToDark = Animator.StringToHash(_animationClipToDark.name);
        _animationHashClipToTransparent = Animator.StringToHash(_animationClipToTransparent.name);;
        _animationHashClipToIdle = Animator.StringToHash(_animationClipToIdle.name);;

        _dayTransitionTextEmitter.Perform.AddListener(TextInDay);
        _nightTransitionTextEmitter.Perform.AddListener(TextInNight);

        _transitionToDarkEvent.WorkStart.AddListener(TransitionToDark);
        _transitionToTransparentEvent.WorkStart.AddListener(TransitionToTransparent);
        _transitionResetEvent.WorkStart.AddListener(ResetTransition);
    }

    void OnDestroy()
    {
        _dayTransitionTextEmitter.Perform.RemoveListener(TextInDay);
        _nightTransitionTextEmitter.Perform.RemoveListener(TextInNight);

        _transitionToDarkEvent.WorkStart.RemoveListener(TransitionToDark);
        _transitionToTransparentEvent.WorkStart.RemoveListener(TransitionToTransparent);
        _transitionResetEvent.WorkStart.RemoveListener(ResetTransition);
    }
    
}
