using UnityEngine;

/// <summary>
/// Clase temporizador 
/// </summary>
[System.Serializable]
public class Cooldown
{
    public Cooldown(float time)
    {
        _cooldownTime = time;
    }


    [SerializeField]
    private float _cooldownTime = 10f;

    public float cooldownTime { get { return _cooldownTime; } }

    private float _nextTime;

    public bool IsCooling() => (Time.time <= _nextTime);

    public void StartCooldown() => _nextTime = Time.time + _cooldownTime;
}
