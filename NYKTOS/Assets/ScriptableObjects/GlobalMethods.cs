using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GlobalMethods", menuName = "GlobalMethods")]
public class GlobalMethods: ScriptableObject
{

    [SerializeField]
    private IntEmitter _sceneLoadEmitter;

    public void PauseAction(bool condition)
    {
        Time.timeScale = condition ? 0.0f : 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
