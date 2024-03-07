using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsDeadCondition : MonoBehaviour, ICondition
{
    public bool Validate(GameObject game)
    {
        return PlayerStateMachine.playerState == PlayerState.Dead;
    }
}
