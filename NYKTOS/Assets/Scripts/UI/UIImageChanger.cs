using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void ChangeImage(WeaponScriptableObject weapon)
    {
        image.sprite = weapon.weaponImage;
    }
}
