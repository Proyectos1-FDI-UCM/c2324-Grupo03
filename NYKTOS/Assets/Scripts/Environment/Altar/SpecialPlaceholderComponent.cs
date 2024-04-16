using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlaceholderComponent : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter _registerPlaceholder;

    [SerializeField]
    private BoolEmitter _placeholderBuilt;


    public void PlaceholderBuilt() => _placeholderBuilt.InvokePerform(true);
    public void PlaceholderDestroyed() => _placeholderBuilt.InvokePerform(false);

    void Start()
    {

        _registerPlaceholder.InvokePerform();
    }

}
