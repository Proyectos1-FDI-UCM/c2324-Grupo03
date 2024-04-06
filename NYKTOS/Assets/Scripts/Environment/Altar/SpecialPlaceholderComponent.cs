using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlaceholderComponent : MonoBehaviour
{
    public enum placeholderType
    {
        yellow, magenta, cyan
    }

    [SerializeField]
    private placeholderType _type;
    public placeholderType type { get { return _type; } }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
