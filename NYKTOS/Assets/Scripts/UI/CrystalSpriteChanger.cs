using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cambia el precio en el menú de defensas para ajustarse al color de cristal correspondiente
/// </summary>
public class CrystalSpriteChanger : MonoBehaviour
{
    [SerializeField]
    private PhTypeEmitter _crystalColor;

    [SerializeField]
    private Sprite _yellowCrystal;
    [SerializeField]
    private Sprite _cyanCrystal;
    [SerializeField]
    private Sprite _magentaCrystal;

    private Image _image;

    private void ChangeSprite(placeholderType color)
    {
        _image.sprite = color switch
        {
            placeholderType.yellow => _yellowCrystal,
            placeholderType.cyan => _cyanCrystal,
            placeholderType.magenta => _magentaCrystal,
            _ => _yellowCrystal,
        };
    }

    void Start()
    {
        _image = GetComponent<Image>();
        _crystalColor.Perform.AddListener(ChangeSprite); 
    }

    private void OnDestroy()
    {
        _crystalColor.Perform.RemoveListener(ChangeSprite);
    }
}
