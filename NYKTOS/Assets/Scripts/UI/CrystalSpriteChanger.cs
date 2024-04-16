using UnityEngine;
using UnityEngine.UI;

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
        switch(color) {
            case placeholderType.yellow:
                _image.sprite = _yellowCrystal; break;
            case placeholderType.cyan:
                _image.sprite = _cyanCrystal; break;
            case placeholderType.magenta:
                _image.sprite = _magentaCrystal; break;
            default:
                _image.sprite = _yellowCrystal; break;
        }
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
