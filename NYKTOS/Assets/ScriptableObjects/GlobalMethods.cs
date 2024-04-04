using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Global Methods", menuName = "Global Methods")]
public class GlobalMethods: ScriptableObject
{
    public static void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
