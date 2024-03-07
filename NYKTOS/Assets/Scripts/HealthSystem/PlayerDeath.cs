using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour, IDeath
{
    #region references
    private PlayerStateMachine _playerState;
    #endregion


    [SerializeField]
    private SpriteRenderer _deathskin;

    [SerializeField]
    private SpriteRenderer _aliveskin;

    public bool alive = true;

    public void Death()
    {
       _aliveskin.enabled = false;
       _deathskin.enabled = true;
       alive = false;
    }

    public void Revive()
    {
        _aliveskin.enabled = true;
        _deathskin.enabled = false;
        alive = true;
    }
}
