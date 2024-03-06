using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class GlobalLightcycle : MonoBehaviour
{

    private Light2D _globalLight;
    // Start is called before the first frame update
    void Start()
    {
        _globalLight = GetComponent<Light2D>();

        GameManager.Instance.GameStateChanged.AddListener(GlobalLightSwitch);

        _globalLight.intensity = (GameManager.Instance.State == GameState.Night) ? 0f : 1f;
    }

    void GlobalLightSwitch(GameState state)
    {
        if(state == GameState.Night)
        {
            _globalLight.intensity = 0;
        }
        else if (state == GameState.Day)
        {
            
            _globalLight.intensity = 1;
        }
    }
}
