using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{
    
    #region emmiters
    [SerializeField]
    private VoidEmitter _playerDeathEmitter;
    [SerializeField]
    private VoidEmitter _playerReviveEmitter;
    [SerializeField]
    private VoidEmitter _nexusMenuEmitter;
    [SerializeField]
    private BoolEmitter _nexusInteractEmitter;
    #endregion

    private BuildingStateMachine _state;

    // Solo afecta a si se abre o no el menu. El evento de revivir se emite siempre
    private void CanInteract(bool canInteract) => _state.isInteractable = canInteract;

    //[Marco] Esto es una guarrada? tal vez pero es lo que hay
    private bool _canRevive = false;
    private void CanRevive() => _canRevive = true;

    public void OpenMenu()
    {
        if (_state.isInteractable)
        {
            //[Marco] Deprecated
            //MenuManager.Instance.OpenNexusMenu();

            _nexusMenuEmitter.InvokePerform();
        }
        else
        {
            // [Marco]
            // Repito que es horripilante, 
            // me da palo ponerme a hacer una maquina de estados del nexo
            if(_canRevive)
            {
                _canRevive = false;
                _playerReviveEmitter.InvokePerform();
            }
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    void Start()
    {
        BuildingManager.Instance.AddBuilding(gameObject);

        _nexusInteractEmitter.Perform.AddListener(CanInteract);
        _playerDeathEmitter.Perform.AddListener(CanRevive);
    }

    void OnDestroy()
    {
        _nexusInteractEmitter.Perform.RemoveListener(CanInteract);
        _playerDeathEmitter.Perform.RemoveListener(CanRevive);
    }
}
