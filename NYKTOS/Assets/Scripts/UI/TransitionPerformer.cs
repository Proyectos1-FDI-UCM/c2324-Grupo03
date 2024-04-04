using UnityEngine;

public class TransitionPerformer : MonoBehaviour
{

    private Animator _animator;

    [SerializeField]
    private GameObject _TransitionToDay;

    [SerializeField]
    private GameObject _TransitionToNight;

    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    private void TransitionToDark(int time)
    {
        _animator.Play("Base Layer.Crossfade_Start", 0, time);
    }

    private void TransitionToTransparent(int time)
    {
        _animator.Play("Base Layer.Crossfade_End", 0, time);
    }

    private void ResetTransition()
    {
        _animator.Play("Base Layer.Crossfade_Idle", 0, 0);
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
}
