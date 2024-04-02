using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class GlobalLightcycle : MonoBehaviour
{

    [SerializeField]
    private InversionEffect _inversionEffect;

    private Light2D _globalLight;
    // Start is called before the first frame update
    void Start()
    {
        _globalLight = GetComponent<Light2D>();
        _inversionEffect.InversionEvent.AddListener(Invert);
    }

    private void Invert(bool inversionStatus)
    {
        if(inversionStatus)
        {
            _globalLight.intensity = 0;
        }
        else
        {
            _globalLight.intensity = 1;
        }
    }
}
