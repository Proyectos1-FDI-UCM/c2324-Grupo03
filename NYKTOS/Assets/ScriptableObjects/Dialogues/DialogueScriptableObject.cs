using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField]
    private string[] _dialogueBoxes;

    [SerializeField] private bool playerCanMove = true;

    public string[] dialogueBoxes { get { return _dialogueBoxes; } }

    private UnityEvent<string[], bool> _dialogueStarted = new UnityEvent<string[], bool>();
    public UnityEvent<string[], bool> dialogueStarted { get { return _dialogueStarted; } }

    public void StartDialogue()
    {
        _dialogueStarted?.Invoke(_dialogueBoxes, playerCanMove);
    }
}
