using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class audiopruebaBORRAR : MonoBehaviour
{
    [SerializeField]
    private UnityEvent a;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            a.Invoke();
        }
    }
}
