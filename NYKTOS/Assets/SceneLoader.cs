using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private IntEmitter _sceneLoadEmitter;

    void Start()
    {
        _sceneLoadEmitter.Perform.AddListener(SceneLoad);
    }

    void OnDestroy()
    {
        _sceneLoadEmitter.Perform.RemoveListener(SceneLoad);
    }

    private void SceneLoad(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
    }
}
