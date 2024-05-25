using UnityEngine;
/// <summary>
/// Clase responsable de cambiar el sprite del Nexus en funcion de si es de dia o de noche, 
/// se podria utilizar para otros objetos que tengan sprites distintos dependiendo de si es de dia o de noche
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

    void Start() 
    {
        currentRenderer = GetComponent<SpriteRenderer>();
        currentRenderer.sprite = defaultSprite;
        inversionEffect.Perform.AddListener(ChangeSprite);
    }

    void OnDestroy()
    {
        inversionEffect.Perform.RemoveListener(ChangeSprite);
    }

    /// <summary>
    /// Metedo responsable de cambiar al sprite de dia o al sprite de noche
    /// </summary>
    /// <param name="swapCondition"> Booleano que indica si es de dia o de noche, si es true es de noche y en caso contrario de dia </param>
    private void ChangeSprite(bool swapCondition) 
    {
        if (swapCondition) 
        {
            currentRenderer.sprite = inversionSprite;
        } else 
        {
            currentRenderer.sprite = defaultSprite;
        }
    }
}
