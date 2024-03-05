using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject[] _menuList;

    [SerializeField]
    private Button[] _firstSelected;
    #endregion

    private int _menuId = 0;

    public void OpenMenu(int newId)
    {
        foreach (GameObject menu in _menuList) menu.SetActive(false);
        _menuId = newId;
        _menuList[_menuId].SetActive(true);
        _firstSelected[_menuId].Select();
    }

    public void CloseMenu()
    {
        _menuList[_menuId].SetActive(false);
    }
}
