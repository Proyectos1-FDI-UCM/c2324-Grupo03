using UnityEngine;

/// <summary>
/// Script que controla la inversi�n de colores de los objetos en el ciclo de d�a/noche.
/// 
/// Esto se aplica a objetos que en la noche cambia su sprite y no cambia simplemente sus colores
/// 
/// </summary>

[RequireComponent(typeof(SpriteRenderer))]
public class NexusSpriteChanger : MonoBehaviour
{
    [SerializeField]
    private BoolEmitter inversionEffect;

    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite inversionSprite;

    SpriteRenderer currentRenderer;

    void Start() {
        currentRenderer = GetComponent<SpriteRenderer>();
        currentRenderer.sprite = defaultSprite;
        inversionEffect.Perform.AddListener(ChangeSprite);
    }

    void OnDestroy()
    {
        inversionEffect.Perform.RemoveListener(ChangeSprite);
    }

    private void ChangeSprite(bool swapCondition) //Cambio de color
    {
        if (swapCondition) {
            currentRenderer.sprite = inversionSprite;
        } else {
            currentRenderer.sprite = defaultSprite;
        }
    }
}
