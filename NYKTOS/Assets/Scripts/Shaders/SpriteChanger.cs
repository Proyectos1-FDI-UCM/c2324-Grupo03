using UnityEngine;
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

    private void ChangeSprite(bool swapCondition) {
        if (swapCondition) {
            currentRenderer.sprite = inversionSprite;
        } else {
            currentRenderer.sprite = defaultSprite;
        }
    }
}
