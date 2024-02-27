using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class LightAreaComponent : MonoBehaviour
{
    #region parameters

    [SerializeField]
    private float lightRadius = 1.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float internalRadiusPercentageOffset = 0.95f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float colliderPercentageOffset = 1.0f;

    #endregion

    #region references

    #region properties

    const float CIRCLE_COLLIDER_EQUIVALENCY = 0.92f;

    #endregion

    private Light2D lightComponent;
    private CircleCollider2D colliderComponent;

    #endregion

    #region methods

    private void DefaultRequiredComponentSettings()
    {
        // Parámetros por defecto de Light2D para todas las luces
        lightComponent.lightType = Light2D.LightType.Point;
        lightComponent.color = Color.white;
        lightComponent.intensity = 1f;
        lightComponent.falloffIntensity = 1f;
        lightComponent.overlapOperation = Light2D.OverlapOperation.AlphaBlend;

        // Parámetros por defecto de Circle collider respecto a un area de luz
        colliderComponent.isTrigger = true;
        colliderComponent.usedByEffector = false;
        colliderComponent.offset = Vector2.zero;
    }

    private void UpdateLightarea()
    {
        lightComponent.pointLightOuterRadius = lightRadius;
        lightComponent.pointLightInnerRadius = 0.95f;
        
        colliderComponent.radius = lightRadius * CIRCLE_COLLIDER_EQUIVALENCY * colliderPercentageOffset;
    }

    public void AugmentArea()
    {

    }

    public void DecreaseArea()
    {

    }

    #endregion

    void Awake()
    {
        lightComponent = GetComponent<Light2D>();
        colliderComponent = GetComponent<CircleCollider2D>();
    }

    void OnValidate()
    {
        #if UNITY_EDITOR
        
        Awake();

        DefaultRequiredComponentSettings();
        UpdateLightarea();
        
        #endif
    }
}
