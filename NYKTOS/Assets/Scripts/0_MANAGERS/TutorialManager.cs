using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImageControllerScript : MonoBehaviour
{

    //[Andrea] Review
    #region Opcional (Creo que se puede aprovechar)
    /*
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
    */
    #endregion

    [SerializeField]
    private GameObject[] Tutorials;

    [SerializeField]
    private VoidEmitter TutorialMovement;
    [SerializeField]
    private VoidEmitter TutorialBlink;
    [SerializeField]
    private VoidEmitter TutorialAttack;
    [SerializeField]
    private VoidEmitter TutorialBuild;
    [SerializeField]
    private VoidEmitter TutorialConfirm;
    [SerializeField]
    private VoidEmitter TutorialAltar;
    [SerializeField]
    private VoidEmitter TutorialCompleted;

    //Hay que activar esto cuando se entre en el estado TutorialDay
    private bool OneTimeMovement = true;
    private bool OneTimeBlink = true;

    private bool OneTimeAttack = false;
    private bool OneTimeBuild = false;
    private bool OneTimeConfirm = false;
    private bool OneTimeAltar = false;
    //Si el jugador interactua con el altar, el tutorial se skipea instantaneamente
    private bool OneTimeCompleted = true;

    private void DisableTutorials()
    {
        foreach (GameObject tutorial in Tutorials)
        {
            tutorial.SetActive(false);
        }
    }

    [ContextMenu("ShowTutorialMovement")]
    private void ShowTutorialMovement()
    {
        if (OneTimeMovement)
        {
            //Aparece al inicial la escena de tutorial Day
            DisableTutorials();
            Tutorials[0].SetActive(true);
            //Desaparece al moverse
            OneTimeBlink = true;
        }

    }

    [ContextMenu("ShowTutorialBlink")]
    private void ShowTutorialBlink()
    {
        if (OneTimeBlink)
        {
            //Aparece al moverse
            DisableTutorials();
            Tutorials[1].SetActive(true);
            //Desaparece al blinkear
            OneTimeMovement = false;
            OneTimeAttack = true;
        }

    }

    [ContextMenu("ShowTutorialAttack")]
    private void ShowTutorialAttack()
    {
        if (OneTimeAttack)
        {
            //Aparece al blinkear
            DisableTutorials();
            Tutorials[2].SetActive(true);
            //Desaparece al atacar
            OneTimeBlink = false;
            OneTimeBuild = true;
        }
    }

    [ContextMenu("ShowTutorialBuild")]
    private void ShowTutorialBuild()
    {
        if (OneTimeBuild)
        {
            //Aparece al atacar
            DisableTutorials();
            Tutorials[3].SetActive(true);
            //Desaparece al construir
            OneTimeAttack = false;
            OneTimeConfirm = true;
        }

    }

    [ContextMenu("ShowTutorialConfirm")]
    private void ShowTutorialConfirm()
    {
        if (OneTimeConfirm)
        {
            //Aparece al construir
            DisableTutorials();
            Tutorials[4].SetActive(true);
            //Desaparece al confirmar
            OneTimeBuild = false;
            OneTimeAltar = true;
        }

    }

    [ContextMenu("ShowTutorialAltar")]
    private void ShowTutorialAltar()
    {
        if (OneTimeAltar)
        {
            //Aparece al confirmar
            DisableTutorials();
            Tutorials[5].SetActive(true);
            //Desaparece al acabar el estado de dia
            OneTimeConfirm = false;
        }
    }

    [ContextMenu("TutorialCompleted")]
    private void ShowTutorialCompleted()
    {
        if (OneTimeCompleted)
        {
            DisableTutorials();
            OneTimeAltar = false;
        }
        
    }

    private void Start()
    {
        TutorialMovement.Perform.AddListener(ShowTutorialMovement);
        TutorialBlink.Perform.AddListener(ShowTutorialBlink);
        TutorialAttack.Perform.AddListener(ShowTutorialAttack);
        TutorialBuild.Perform.AddListener(ShowTutorialBuild);
        TutorialConfirm.Perform.AddListener(ShowTutorialConfirm);
        TutorialAltar.Perform.AddListener(ShowTutorialAltar);
        TutorialCompleted.Perform.AddListener(ShowTutorialCompleted);
    }
}