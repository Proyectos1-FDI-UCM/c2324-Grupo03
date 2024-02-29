using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(HealthComponent))]
public class SetOnFireDebuff : MonoBehaviour
{
    #region parameters
    //el tiempo que transcurra entre cada golpe sera de deactivateDebuffTime / numberOfHitsBeforeDeactivation
    [SerializeField] private float deactivateDebuffTime = 2f;
    [SerializeField] private int numberOfHitsBeforeDeactivation = 4;
    [SerializeField] private int burnDamage = 1;
    #endregion

    #region references
    private HealthComponent _myHealthComponent;
    #endregion
    private void OnEnable()
    {
        Invoke(nameof(Deactivate), deactivateDebuffTime);
        StartCoroutine(Burn());
    }

    private void Deactivate()
    {
        this.enabled = false;
    }

    IEnumerator Burn()
    {
        for (int i = 0; i < numberOfHitsBeforeDeactivation; i++)
        {
            yield return new WaitForSeconds(deactivateDebuffTime / numberOfHitsBeforeDeactivation);
            _myHealthComponent.Damage(burnDamage);
            
        }
    }

    private void Awake()
    {
        _myHealthComponent = GetComponent<HealthComponent>();
        this.enabled = false;
    }
}
