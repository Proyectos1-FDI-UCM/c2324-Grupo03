using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollaboratorUnlocker : CollaboratorWorker
{
    [SerializeField]
    private float _unlockWaitTime = 0.1f;

    protected override IEnumerator Perform()
    {
        yield return new WaitForSeconds(_unlockWaitTime);
    }
}
