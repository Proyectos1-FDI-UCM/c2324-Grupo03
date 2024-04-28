using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    [SerializeField]
    private GameStateMachine _stateMachine;

    [SerializeField]
    private CustomState _winSceneState;

    [SerializeField]
    private List<BoolEmitter> _altarEmittersList = new List<BoolEmitter>();
    private List<bool> _altars = new List<bool>();

    private int _builtAltars = 0;

    void Start()
    {
        _altars.Capacity = _altarEmittersList.Count;

        for (int i = 0; i < _altarEmittersList.Count; i++)
        {
            int altarIndex = i;
            _altarEmittersList[i].Perform.AddListener
            (
                ( bool altarState ) => 
                {   
                    SetBuiltAltar(altarState, altarIndex); 
                }
            );
            
            _altars.Add(false);
        };
    }

    private void SetBuiltAltar(bool state, int altarIndex)
    {
        if (_altars[altarIndex] != state) 
        {
            _altars[altarIndex] = state;

            if (state)
            {
                _builtAltars++;
                if (_builtAltars >= _altars.Count)
                {
                    _stateMachine.SetState(_winSceneState);
                }
            }
            else
            {
                _builtAltars--;
            }
        }
    }
}