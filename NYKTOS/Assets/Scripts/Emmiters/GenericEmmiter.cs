using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "New Emitter", menuName = "Emitter/Generic")]
public class GenericEmitter<T> : ScriptableObject
{
    private UnityEvent<T> _perform = new UnityEvent<T>();
    public UnityEvent<T> Perform{get{return _perform;}}

    public void InvokePerform(T eventData)
    {
        _perform.Invoke(eventData);
    }
}

public class GenericEmitter : ScriptableObject
{
    private UnityEvent _perform = new UnityEvent();
    public UnityEvent Perform{get{return _perform;}}

    public void InvokePerform()
    {
        _perform.Invoke();
    }
}

public class CreateGenericEmitterMenu
{
    [MenuItem("Assets/Create/Emitter/Generic/Void Emitter")]
    public static void CreateVoidEmitter()
    {
        CreateEmitter<int>();
    }

    [MenuItem("Assets/Create/Emitter/Generic/Int Emitter")]
    public static void CreateIntEmitter()
    {
        CreateEmitter<int>();
    }

    [MenuItem("Assets/Create/Emitter/Generic/Float Emitter")]
    public static void CreateFloatEmitter()
    {
        CreateEmitter<float>();
    }

    private static void CreateEmitter<T>()
    {
        GenericEmitter<T> emitter = ScriptableObject.CreateInstance<GenericEmitter<T>>();
        string typeName = typeof(T).Name;
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }
        else if (!string.IsNullOrEmpty(Path.GetExtension(path)))
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/New{typeName}Emitter.asset");
        AssetDatabase.CreateAsset(emitter, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = emitter;
    }
}