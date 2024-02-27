using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Esto creo que es para editar los parámetros serializados desde el script que tenga a este referenciado
public class Cooldown
{

    [SerializeField]
    private float _cooldownTime = 10f;

    public float cooldownTime { get { return _cooldownTime; } }

    private float _nextTime;

    public bool IsCooling() => (Time.time <= _nextTime);

    public void StartCooldown() => _nextTime = Time.time + _cooldownTime;
}
