using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImageControllerScript : MonoBehaviour
{

    
    
    [SerializeField]
    private GameObject _allTutorials;
    [SerializeField]
    private List<GameObject> _tutorials;
    [SerializeField]
    private float _appearTime = 2f;
    [SerializeField]
    private float _disappearTime = 1f;

    IEnumerator Start()
    {
        
            yield return StartCoroutine(AppearAndDisappearImages());
            _allTutorials.SetActive(true);
    }

    IEnumerator AppearAndDisappearImages()
    {
        
        foreach (GameObject tutorial in _tutorials)
        {
            if (GameManager.Instance.State == GameState.Night)
            {
                _allTutorials.SetActive(false);
            }

            yield return new WaitForSeconds(_disappearTime);

            tutorial.SetActive(true); // Hace que la imagen aparezca

            yield return new WaitForSeconds(_appearTime);

            tutorial.SetActive(false); // Hace que la imagen desaparezca

            
        }
    }
}