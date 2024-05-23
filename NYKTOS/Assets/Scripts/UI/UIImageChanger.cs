using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cambia la apariencia del arma en el HUD
/// </summary>
public class UIImageChanger : MonoBehaviour
{
    #region references
    [SerializeField] private WeaponSOEmmiter _emitter;
    Image image;
    #endregion

    private void Start()
    {
        _emitter.Perform.AddListener(ChangeImage);
        image = GetComponent<Image>();
    }

    void OnDestroy()
    {
        _emitter.Perform.RemoveListener(ChangeImage);
    }

    private void ChangeImage(WeaponScriptableObject weapon)
    {
        image.sprite = weapon.weaponImage;
    }
}
