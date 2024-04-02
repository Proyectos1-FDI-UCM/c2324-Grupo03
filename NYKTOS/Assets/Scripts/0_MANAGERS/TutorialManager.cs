using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImageControllerScript : MonoBehaviour
{

    //[Marco] Not optimal
    [SerializeField]
    private GameStateMachine _stateMachine;
    
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
            if (_stateMachine.GetCurrentState == GlobalStateIdentifier.Night)
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