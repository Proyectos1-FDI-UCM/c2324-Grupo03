using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Este componente centraliza Light2D y CircleCollider2D de tal forma
/// que se pueden modificar ambos en este mismo componente con los parámetros
/// serializables, el único propósito de esta clase es tener facilidad de control
/// </summary>
[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class LightAreaComponent : MonoBehaviour
{
    #region parameters

    [SerializeField]
    private float lightRadius = 1.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float internalRadiusPercentageOffset = 0.98f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float colliderPercentageOffset = 1.0f;

    #endregion

    #region references

    private Light2D lightComponent;
    private CircleCollider2D colliderComponent;

    #endregion

    #region properties

    [SerializeField]
    const float CIRCLE_COLLIDER_EQUIVALENCY = 0.92f;

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
        lightComponent.pointLightInnerRadius = lightRadius * internalRadiusPercentageOffset;
        
        colliderComponent.radius = lightRadius * CIRCLE_COLLIDER_EQUIVALENCY * colliderPercentageOffset;
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
