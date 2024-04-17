using UnityEngine;

public class LoadButtonEnabler : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(ProgressData.SaveFileExists());
    }
}
