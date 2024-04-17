using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescriptionMenus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {

    [SerializeField]
    private GameObject _description;

    public void OnPointerEnter(PointerEventData eventData) {

        _description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {

        _description.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _description.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _description.SetActive(false);
    }
}
