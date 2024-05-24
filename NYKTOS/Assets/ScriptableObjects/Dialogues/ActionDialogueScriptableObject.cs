using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Almacena informacion de los action dialogues. El Dialogue System coge todo lo que necesita de aqui.
/// Se encuentran cosas como el texto que se reproduce, la accion que se necesita para cumplir con el dialogo, la voz que suena...
/// </summary>
[CreateAssetMenu(fileName = "New Action Dialogue", menuName = "Dialogue/Action Dialogue")]
public class ActionDialogueScriptableObject : ScriptableObject
{
    [TextArea(5, 20)]
    [SerializeField]
    private string _dialogueBox;

    [SerializeField] private AudioPlayer _voice;
    public AudioPlayer voice { get { return _voice; } }

    [SerializeField] private UnityEvent _onDialogueStart = new UnityEvent();
    [SerializeField] private UnityEvent _onDialogueFinish = new UnityEvent();

    public string dialogueBox { get { return _dialogueBox; } }

    private UnityEvent<string, ActionDialogueScriptableObject, VoidEmitter> _dialogueStarted = new UnityEvent<string, ActionDialogueScriptableObject, VoidEmitter>();
    public UnityEvent<string, ActionDialogueScriptableObject, VoidEmitter> dialogueStarted { get { return _dialogueStarted; } }

    [SerializeField] private VoidEmitter _voidEmitterAction;
    public VoidEmitter voidEmitterAction { get { return _voidEmitterAction; } }

    public void StartDialogue()
    {
        _dialogueStarted?.Invoke(_dialogueBox, this, _voidEmitterAction);
    }

    public void PlayFinishEvent()
    {
        _onDialogueFinish?.Invoke();
    }

    public void PlayEnterEvent()
    {
        _onDialogueStart?.Invoke();
    }
}
