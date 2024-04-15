using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField]
    private string[] _dialogueBoxes;

    public string[] dialogueBoxes { get { return _dialogueBoxes; } }

    private UnityEvent<string[]> _dialogueStarted = new UnityEvent<string[]>();
    public UnityEvent<string[]> dialogueStarted { get { return _dialogueStarted; } }

    public void StartDialogue()
    {
        _dialogueStarted?.Invoke(_dialogueBoxes);
    }
}
