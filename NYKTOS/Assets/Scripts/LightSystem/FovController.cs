using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FovController : MonoBehaviour
{
    #region parameters

    [SerializeField]
    public float maxLightRadius = 3.5f;
    [SerializeField]
    public float minLightRadius = 1.5f;
    [SerializeField]
    public float increaseFactor = 2f;
    [SerializeField]
    public float decreaseFactor = 1f;

    #endregion

    #region references
    
    private Light2D playerLight;

    #endregion

    #region properties

    private int insideLightAreas = 0;

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
    }

    void Update() {

        float currentRadius = playerLight.pointLightOuterRadius;

        if (insideLightAreas > 0 ) 
        {
            playerLight.pointLightOuterRadius = 
                Mathf.Clamp 
                (
                    currentRadius + (increaseFactor * Time.deltaTime), 
                    minLightRadius, 
                    maxLightRadius
                );
        }
        else {
            playerLight.pointLightOuterRadius = 
                Mathf.Clamp 
                (
                    currentRadius - (decreaseFactor * Time.deltaTime), 
                    minLightRadius, 
                    maxLightRadius
                );
        }
    }
}
