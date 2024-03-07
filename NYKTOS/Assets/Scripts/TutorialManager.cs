using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImageControllerScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _tutorials;
    [SerializeField]
    private float _appearTime = 2f;
    [SerializeField]
    private float _disappearTime = 1f;

    IEnumerator Start()
    {
        
            yield return StartCoroutine(AppearAndDisappearImages());
        
    }

    IEnumerator AppearAndDisappearImages()
    {
        foreach (GameObject tutorial in _tutorials)
        {
            yield return new WaitForSeconds(_disappearTime);

            tutorial.SetActive(true); // Hace que la imagen aparezca

            yield return new WaitForSeconds(_appearTime);

            tutorial.SetActive(false); // Hace que la imagen desaparezca

            
        }
    }
}