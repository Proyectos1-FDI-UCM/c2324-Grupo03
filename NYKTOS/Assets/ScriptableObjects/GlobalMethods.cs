using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalMethods", menuName = "GlobalMethods")]
public class GlobalMethods: ScriptableObject
{
    public static void PauseAction(bool condition)
    {
        Time.timeScale = condition ? 0.0f : 1.0f;
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
