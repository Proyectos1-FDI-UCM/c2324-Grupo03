using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Escucha al evento de cambio día a noche.
/// 
/// Según si es de día o de noche la luz global es 1 o 0
/// </summary>
[RequireComponent(typeof(Light2D))]
public class GlobalLightcycle : MonoBehaviour
{

    [SerializeField]
    private BoolEmitter _inversionEffect;

    private Light2D _globalLight;

    void Start()
    {
        _globalLight = GetComponent<Light2D>();
        _inversionEffect.Perform.AddListener(Invert);
    }

    void OnDestroy()
    {
        _inversionEffect.Perform.RemoveListener(Invert);
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
