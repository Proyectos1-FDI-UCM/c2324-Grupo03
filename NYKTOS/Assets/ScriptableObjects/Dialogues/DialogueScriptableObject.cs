using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Talking Dialogue", menuName = "Dialogue/Talking Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField]
    private string[] _dialogueBoxes;
    [SerializeField] private UnityEvent _onDialogueFinish = new UnityEvent();

    public string[] dialogueBoxes { get { return _dialogueBoxes; } }

    private UnityEvent<string[], DialogueScriptableObject> _dialogueStarted = new UnityEvent<string[], DialogueScriptableObject>();
    public UnityEvent<string[], DialogueScriptableObject> dialogueStarted { get { return _dialogueStarted; } }

    public void StartDialogue()
    {
        _dialogueStarted?.Invoke(_dialogueBoxes, this);
    }

    public void PlayFinishEvent()
    {
        _onDialogueFinish?.Invoke();
    }
}
