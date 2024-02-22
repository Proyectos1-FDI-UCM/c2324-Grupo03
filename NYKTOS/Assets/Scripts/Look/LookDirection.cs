using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection : MonoBehaviour
{
    public Vector2 lookDirection = Vector2.down;

    public void SetLookDirection(Vector2 dir)
    {
        if(dir != Vector2.zero) lookDirection = dir;
    }
}
