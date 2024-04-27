using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteract : MonoBehaviour
{
    [SerializeField]
    private BoolEmitter _canInteract;

    private Button _button;

    //private void CanInteract(bool value) => _button.interactable = value;
    private void CanInteract(bool value)
    {
        _button.interactable = value;
    }

    void Start()
    {
        _button = GetComponent<Button>();
        _canInteract.Perform.AddListener(CanInteract);
    }

    private void OnDestroy()
    {
        _canInteract.Perform.RemoveListener(CanInteract);
    }
}
