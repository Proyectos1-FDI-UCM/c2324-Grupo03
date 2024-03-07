using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsAliveCondition : MonoBehaviour, ICondition
{
    public bool Validate(GameObject game)
    {
        return PlayerStateMachine.playerState != PlayerState.Dead;
    }
}
