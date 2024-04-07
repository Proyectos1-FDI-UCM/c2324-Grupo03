using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GlobalMethods", menuName = "GlobalMethods")]
public class GlobalMethods: ScriptableObject
{

    public void PauseAction(bool condition)
    {
        Time.timeScale = condition ? 0.0f : 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void DebugText()
    {
        Debug.Log("DEBUGTEXT");
    }
}
