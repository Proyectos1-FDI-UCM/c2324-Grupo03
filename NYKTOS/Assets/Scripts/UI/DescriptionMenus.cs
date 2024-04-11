using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DescriptionMenus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [SerializeField]
    private TMP_Text _descriptionText;
    [SerializeField]
    private RawImage _backGround;

    public void OnPointerEnter(PointerEventData eventData) {
        
        _descriptionText.gameObject.SetActive(true);
        _backGround.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {

        _descriptionText.gameObject.SetActive(false);
        _backGround.gameObject.SetActive(false);
    }
}
