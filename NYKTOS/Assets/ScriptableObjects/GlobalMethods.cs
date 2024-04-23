using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// Clase cuyo único propósito es tener métodos que se pueden llamar
/// desde eventos serializados en el editor
/// 
/// </summary>
[CreateAssetMenu(fileName = "GlobalMethods", menuName = "GlobalMethods")]
public class GlobalMethods: ScriptableObject
{
    // Creo que estos dos se entienden bastante bien lo que hacen

    public void PauseAction(bool condition)
    {
        Time.timeScale = condition ? 0.0f : 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 
    /// Activa la flag para marcar al cargador de archivos de guardado que esta es una nueva partida,
    /// de esa forma ignorará el archivo que haya si es que existe
    /// 
    /// </summary>
    public void ActivateNewGame()
    {
        ProgressLoader.ActivateNewGameFlag();
    }
}
