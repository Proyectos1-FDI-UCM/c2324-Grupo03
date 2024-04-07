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
    private CollaboratorEvent _TransitionToDark;
    [SerializeField]
    private CollaboratorEvent _TransitionToTransparent;
    [SerializeField]
    private CollaboratorEvent _TransitionReset;
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

    #region Actions
    private UnityAction _actionDark;
    private UnityAction _actionTransparent;
    private UnityAction _actionIdle;
    #endregion

    private void StartWorker(CollaboratorEvent collaboratorEvent, Func<IEnumerator> coroutine)
    {
        collaboratorEvent.AddWorker();
        StartCoroutine(WorkerCorroutine(collaboratorEvent, coroutine));
    }

    private IEnumerator WorkerCorroutine(CollaboratorEvent collaboratorEvent, Func<IEnumerator> coroutine)
    {
        yield return coroutine;
        collaboratorEvent.DeleteWorker();
    }

    private IEnumerator TransitionTo(AnimationClip animationClip)
    {
        if (_animator != null && animationClip != null)
        {
            print("animacion lanzada");
            // Obtenemos el hash del nombre del clip de animaci�n
            int transitionHash = Animator.StringToHash(animationClip.name);
            // Reproducimos la animaci�n utilizando el hash
            _animator.Play(transitionHash, 0, 0f);

            yield return new WaitForSeconds(animationClip.length);
        }
        yield return null;
    }

    [ContextMenu("TransitionToDark")]
    private IEnumerator TransitionToDark()
    {
        yield return TransitionTo(AnimationClipToDark);
    }

    [ContextMenu("TransitionToTransparent")]
    private IEnumerator TransitionToTransparent()
    {
        yield return TransitionTo(AnimationClipToTransparent);
    }

    [ContextMenu("ResetTransition")]
    private IEnumerator ResetTransition()
    {
        yield return TransitionTo(AnimationClipToIdle);
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
        _actionDark = () => StartWorker(_TransitionToDark, TransitionToDark);
        _actionTransparent = () => StartWorker(_TransitionToTransparent, TransitionToTransparent);
        _actionIdle = () => StartWorker(_TransitionReset, ResetTransition);

        _animator = GetComponent<Animator>();

        _DayTransitionText.Perform.AddListener(TextInDay);
        _NightTransitionText.Perform.AddListener(TextInNight);

        _TransitionToDark.WorkStart.AddListener(_actionDark);
        _TransitionToTransparent.WorkStart.AddListener(_actionTransparent);
        _TransitionReset.WorkStart.AddListener(_actionIdle);
    }

    void OnDestroy()
    {
        _DayTransitionText.Perform.RemoveListener(TextInDay);
        _NightTransitionText.Perform.RemoveListener(TextInNight);

        _TransitionToDark.WorkStart.RemoveListener(_actionDark);
        _TransitionToTransparent.WorkStart.RemoveListener(_actionTransparent);
        _TransitionReset.WorkStart.RemoveListener(_actionIdle);
    }
    
}
