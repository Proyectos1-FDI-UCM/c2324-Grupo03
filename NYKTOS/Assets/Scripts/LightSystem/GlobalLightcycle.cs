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

        GameManager.Instance.GameStateChanged.AddListener(GameStateListener);

        GameStateListener(GameManager.Instance.State);
    }

    private void GameStateListener(GameState state)
    {
        if(state == GameState.Night)
        {
            _globalLight.intensity = 0;
            _inversionEffect.Invert(true);
        }
        else if (state == GameState.Day)
        {
            _globalLight.intensity = 1;
            _inversionEffect.Invert(false);
        }
    }
}
