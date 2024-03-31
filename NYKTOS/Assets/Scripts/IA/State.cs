using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

[System.Serializable]
struct stateAndConditions
{
    public State state;
    public ConditionChecker[] enterConditions;
}

public class State : MonoBehaviour
{
    #region animations
    [Header("ANIMATIONS")]
    [SerializeField]
    string _animationName = string.Empty;
    #endregion

    #region behaviours
    [SerializeField]
    private BehaviourPerformer[] enterBehaviours;

    [SerializeField]
    private BehaviourPerformer[] updateBehaviours;

    [SerializeField]
    private BehaviourPerformer[] exitBehaviours;
    #endregion

    #region states and conditions

    [SerializeField]
    private stateAndConditions[] possibleStates;
    #endregion

    public void OnEnterState()
    {
        PlayAnimation(_animationName);
        foreach(BehaviourPerformer behaviour in enterBehaviours)
        {
            behaviour.Perform();
        }
    }

    public void OnUpdateState()
    {
        foreach (BehaviourPerformer behaviour in updateBehaviours)
        {
            behaviour.Perform();
        }
    }

    public void OnExitState()
    {
        foreach (BehaviourPerformer behaviour in exitBehaviours)
        {
            behaviour.Perform();
        }
    }

    /// <summary>
    /// Chequea si despues del update se puede pasar a algun estado que tenga asignado currentState, si se retorna true, currentState = state. Si retorna false no se hace nada.
    /// </summary>
    /// <param name="nextState">
    /// Estado que se va a asignar en StateHandler
    /// </param>
    /// <returns></returns>
    public bool CheckConditions(ref State nextState)
    {
        bool condition = false;

        int i = 0;
        while (i < possibleStates.Length && !condition) //se entra en todos los posibles estados a los que se puede ir
        {
            int j = 0;
            bool foundFalse = false;
            while(j < possibleStates[i].enterConditions.Length && !foundFalse) //en cada estado se va a buscar verificar absolutamente todas las condiciones que contenga
            {
                foundFalse = !possibleStates[i].enterConditions[j].Check(this.gameObject);

                j++;
            }
            condition = !foundFalse; //si no se ha falsado ninguna condicion, se ha terminado

            if (!condition) i++;
        }
        if (condition)
        nextState = possibleStates[i].state;
        return condition;
    }

    private void PlayAnimation(string clip)
    {
        if (clip != null && GetComponentInParent<HealthComponent>().GetComponentInChildren<Animator>() != null)
        {
            Animator animator = GetComponentInParent<HealthComponent>().GetComponentInChildren<Animator>();
            animator.Play(clip);
        }
    }
}
