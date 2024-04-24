using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMarkerPerformer : MonoBehaviour
{
    [SerializeField]
    private BoolEmitter _spawnMarkerEmitter;

    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        _spawnMarkerEmitter.Perform.AddListener(ToggleMarker);
    }

    void OnDestroy()
    {
        _spawnMarkerEmitter.Perform.RemoveListener(ToggleMarker);
    }

    private void ToggleMarker(bool toggle)
    {
        _image.enabled = toggle;
    }
}
