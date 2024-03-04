using UnityEngine;
using UnityEngine.Rendering.Universal;


/// <summary>
/// Component que detecta areas de luz y en base a si está o no en un área 
/// aumenta o decrece
/// 
/// <para> 
/// Requiere de un Light2D ya que es lo que se va a iluminar y 
/// un CircleCollider2D para detectar areas de luz
/// </para> 
///
/// <para> 
/// FovController debería ir en un GameObject hijo separada del resto de componentes
/// de la entidad padre.
/// </para> 
/// </summary>
[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FovController : MonoBehaviour
{
    #region parameters

    [Range(0.0f,1.0f)]
    [SerializeField]
    private float internalLightProportion = 0.98f;

    /// <summary>
    /// Radio máximo de iluminación del FOV
    /// </summary>
    [SerializeField]
    private float maxLightRadius = 4f;

    /// <summary>
    /// Radio mínimo de iluminación del FOV
    /// </summary>
    [SerializeField]
    private float minLightRadius = 1.5f;

    /// <summary>
    /// Multiplicador que se aplica al incrementar el FOV
    /// </summary>
    [SerializeField]
    private float increaseMultiplier = 8f;

    /// <summary>
    /// Unidad mínima de incremento y decremento del FOV
    /// </summary>
    [SerializeField]
    private float fearModifyUnit = 0.25f;

    /// <summary>
    /// Multiplicador de decremento cuando existe "miedo provocado"
    /// </summary>
    [SerializeField]
    private float provokedMultiplier = 24f;

    /// <summary>
    /// Cantidad máxima de miedo provocado, no puede ser mayor que maxLightRadius - minLightRadius 
    /// </summary>
    [SerializeField]
    private float provokedFearMax = 1f;

    #endregion

    #region references
    
    private Light2D playerLight;

    #endregion

    #region properties

    /// <summary>
    /// Unidades de rango de area de luz que se tienen que reducir usando
    /// el multiplicador provokedMultiplier sobre el decrecimiento
    /// <para> 
    /// Está serializado para hacer pruebas, esto hay que quitarlo una vez se pruebe
    /// </para> 
    /// </summary>
    [SerializeField]
    public float provokedFear = 0f;

    /// <summary>
    /// Número de areas en las que está la entidad asociada al FOV
    /// </summary>
    private int insideLightAreas = 0;

    #endregion

    #region methods

    /// <summary>
    /// Suma sobre provokedFear hasta un máximo
    /// </summary>
    /// <param name="newFear">Unidades de decrecimiento a sumar</param>
    public void addProvokedFear(float newFear)
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

    /// <summary>
    /// Registra que la entidad asociada ha entrado en un área de luz
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<LightAreaComponent>() != null) 
        {
            insideLightAreas++;
        }
    }

    /// <summary>
    /// Registra que la entidad asociada ha salido en un área de luz
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponent<LightAreaComponent>() != null) 
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

        playerLight.pointLightInnerRadius = playerLight.pointLightOuterRadius * internalLightProportion;
    }


    void OnValidate()
    {
        #if UNITY_EDITOR
        
        minLightRadius = Mathf.Max(0f, minLightRadius);
        maxLightRadius = Mathf.Max(minLightRadius, maxLightRadius);

        GetComponent<Light2D>().pointLightOuterRadius = maxLightRadius;
        GetComponent<Light2D>().pointLightInnerRadius = maxLightRadius * internalLightProportion;

        provokedFearMax = Mathf.Clamp(provokedFearMax, 0f, maxLightRadius - minLightRadius);
        provokedMultiplier = Mathf.Max(1.0f, provokedMultiplier);
        
        #endif
    }
}
