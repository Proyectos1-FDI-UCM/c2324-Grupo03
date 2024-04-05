using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Global Methods", menuName = "Global Methods")]
public class GlobalMethods: ScriptableObject
{
    [SerializeField]
    BoolEmitter _pauseEmitter;

    public static void PauseAction(bool condition)
    {
        Time.timeScale = condition ? 0.0f : 1.0f;
    }

    void OnValidate()
    {
        _pauseEmitter.Perform.AddListener(PauseAction);
    }
}
