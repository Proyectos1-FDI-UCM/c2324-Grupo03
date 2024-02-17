using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELETEinputPrueba : MonoBehaviour
{
    private Movement movement;
    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<Movement>();
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
                movement.Blink();
            }
        }
    }
}
