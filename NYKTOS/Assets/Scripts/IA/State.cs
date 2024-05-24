using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

/// <summary>
/// Struct que contiene el estado al que se quiere ir como las condiciones que se deben cumplir para ello.
/// </summary>
[System.Serializable]
struct stateAndConditions
{
    public State state;
    public ConditionChecker[] enterConditions;
}

/// <summary>
/// Almacena las propiedades del estado:
/// -Los comportamientos que se van a realizar al entrar al estado, durante el estado, y al salir del estado
/// -Posibles estados de salida y sus condiciones
/// -Animaciones que se van a ejecutar a traves de strings
/// </summary>
public class State : MonoBehaviour
{
    #region animations
    [Header("ANIMATIONS")]
    [SerializeField]
    string _animationName = string.Empty;

    private enum AnimationPriority
    {
        Player, Building, Both
    }

    [SerializeField]
    private AnimationPriority _whereToLookAt = AnimationPriority.Player;
    #endregion

    #region behaviours
    [Header("BEHAVIOURS")]
    [SerializeField]
    private BehaviourPerformer[] enterBehaviours;

    [SerializeField]
    private BehaviourPerformer[] updateBehaviours;

    [SerializeField]
    private BehaviourPerformer[] exitBehaviours;
    #endregion

    #region states and conditions
    [Header("EXIT STATES AND CONDITIONS")]

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

    /// <summary>
    /// Reproduce la animacion
    /// Dependiendo de el estado en el que se encuentre se reproduce mirando al jugador, mirando a la torre mas cercana o mirando en direccion a su ruta de movimiento.
    /// </summary>
    /// <param name="clip"></param>
    private void PlayAnimation(string clip)
    {
        if (clip != null && GetComponentInParent<HealthComponent>().GetComponentInChildren<Animator>() != null)
        {
            Animator animator = GetComponentInParent<HealthComponent>().GetComponentInChildren<Animator>();
            animator.Play(clip);

            if(GetComponentInParent<EnemyPriorityComponent>() != null)
            {
                EnemyPriorityComponent priority = GetComponentInParent<EnemyPriorityComponent>();

                Vector3 direction = Vector3.right;

                if (_whereToLookAt == AnimationPriority.Player && priority.toPlayerPath.corners.Length > 1)
                {
                    direction = (priority.toPlayerPath.corners[1] - priority.transform.position).normalized;
                }
                else if (_whereToLookAt == AnimationPriority.Building && priority.toNearestBuildingPath.corners.Length >1)
                {
                    direction = (priority.toNearestBuildingPath.corners[1] - priority.transform.position).normalized;
                }
                else if (priority.priorityPath != null && priority.priorityPath.corners.Length > 0)
                {
                    direction = (priority.priorityPath.corners[1] - priority.transform.position).normalized;
                }

                animator.SetFloat("xAxis", direction.x);
                animator.SetFloat("yAxis", direction.y);
            }
        }
    }
}
