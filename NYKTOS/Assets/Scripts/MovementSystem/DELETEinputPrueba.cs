using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELETEinputPrueba : MonoBehaviour
{
    private RBMovement movement;
    private WeaponHandler weaponHandler;
    private BlinkComponent blinkComponent;
    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<RBMovement>();
        weaponHandler = GetComponent<WeaponHandler>();
        blinkComponent = GetComponentInChildren<BlinkComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                movement.yAxisMovement(1);
                
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movement.yAxisMovement(-1);
            }
            else
            {
                movement.yAxisMovement(0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                movement.xAxisMovement(-1);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                movement.xAxisMovement(1);
            }
            else
            {
                movement.xAxisMovement(0);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                blinkComponent.Blink();
            }
        }

        if(weaponHandler != null)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                weaponHandler.CallPrimaryUse(0);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                weaponHandler.CallSecondaryUse(0);
            }
        }
    }
}
