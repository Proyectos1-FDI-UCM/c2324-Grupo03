using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImageControllerScript : MonoBehaviour
{

    //[Andrea] Review
    [SerializeField]
    private VoidEmitter _day;

    [SerializeField]
    private GameObject _allTutorials;
    [SerializeField]
    private List<GameObject> _tutorials;
    [SerializeField]
    private float _appearTime = 2f;
    [SerializeField]
    private float _disappearTime = 1f;

    private void DisableTutorials() => _allTutorials.SetActive(false);

    IEnumerator Start()
    {
        _day.Perform.AddListener(DisableTutorials);

        yield return StartCoroutine(AppearAndDisappearImages());
        _allTutorials.SetActive(true);
    }

    void OnDestroy()
    {
        _day.Perform.RemoveListener(DisableTutorials);
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