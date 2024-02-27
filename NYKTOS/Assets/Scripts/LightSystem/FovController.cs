using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FovController : MonoBehaviour
{
    #region parameters

    [SerializeField]
    private float maxLightRadius = 3.5f;
    [SerializeField]
    private float minLightRadius = 1.5f;
    [SerializeField]
    private float increaseMultiplier = 1f;

    [SerializeField]
    private float fearModifyUnit = 1f;

    [SerializeField]
    private float provokedMultiplier = 2f;

    [SerializeField]
    private float provokedFearMax = 10f;

    #endregion

    #region references
    
    private Light2D playerLight;

    #endregion

    #region properties

    /// <summary>
    /// Miedo inducido por los enemigos
    /// 
    /// Está serializado para hacer pruebas, esto hay que quitarlo una vez se pruebe
    /// </summary>
    [SerializeField]
    public float provokedFear = 0f;

    private int insideLightAreas = 0;

    #endregion

    #region methods

    
    public void ProvokeFear(float newFear)
    {
        provokedFear = 
            Mathf.Clamp
            (
                provokedFear + newFear,
                0.0f,
                provokedFearMax
            );
    }

    #endregion

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Light2D>() != null) 
        {
            insideLightAreas++;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponent<Light2D>() != null) 
        {
            insideLightAreas--;
        }
    }

    void Start() 
    {
        playerLight = GetComponent<Light2D>();

        playerLight.pointLightOuterRadius = maxLightRadius;
    }

    void Update() 
    {
        float currentDelta = Time.deltaTime;
        float fovModifier;

        if (insideLightAreas > 0) 
        {
            fovModifier = fearModifyUnit * increaseMultiplier * currentDelta;
        }
        else
        {
            fovModifier = 
            (
                (provokedFear > 0f)
                ? 
                    Mathf.Clamp
                    (
                        provokedFear - (fearModifyUnit * provokedMultiplier * currentDelta), 
                        fearModifyUnit * currentDelta, 
                        fearModifyUnit * provokedMultiplier * currentDelta
                    )
                : 
                    fearModifyUnit * currentDelta        
            ) * -1;
                

            provokedFear = (provokedFear > 0) ? provokedFear + fovModifier : 0f;
        }

        playerLight.pointLightOuterRadius = 
            Mathf.Clamp
            (
                playerLight.pointLightOuterRadius + fovModifier, 
                minLightRadius, 
                maxLightRadius
            );
    }
}
