using UnityEngine;
/// <summary>
/// Behaviour de muerte que se encarga de destruir el objeto
/// </summary>
public class DieBehaviour : MonoBehaviour, IBehaviour
{
    public void PerformBehaviour()
    {
        //Debug.Log("[DIE BEHAVIOUR] Muerte]");
        Destroy(GetComponentInParent<HealthComponent>().gameObject);
        
    }
}
