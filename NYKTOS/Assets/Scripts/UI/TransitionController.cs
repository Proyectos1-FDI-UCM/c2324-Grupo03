using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

//Codigo de Iker :D
public class TransitionController : MonoBehaviour
{
    public enum Transitions
    {
        Transition1,
        Transition2,
        Transition3,
        Transition4
    }
    [SerializeField]
    private Transitions TransitionType;


    private Image image;
    private Transform _childTransform;
    private Animator animator;

    [SerializeField]
    private float TransitionDuration = 1.5f;
    private float originalOpacity = 0f;
    private float fullOpacity = 1f;
    private float startTime;
    
    
    //Para pruebas
    public bool Transition = false;
    public bool Transition2 = false;
    public bool Transition3 = false;
    public bool Transition4 = false;
    private bool isTransitioning = false;
    private bool isTransitioningLerp = false;
    private bool isDarkTransition = false;
    private bool Opacity100 = false;
    private bool endTransition = false;
    private bool NormalState = false;

    private void Update()
    {
        if (Transition && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition1)
        {
            TransitionToDark();
        }
        else if (!Transition && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition1)
        {
            TransitionToNormal();
        }

        if (Transition2 && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition2)
        {
            InstantTransitionToDark();
        }
        else if (!Transition2 && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition2)
        {
            InstantTransitionToNormal();
        }

        if (Transition3 && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition3)
        {
            HalfTimeTransitionToDark();
        }
        else if (!Transition3 && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition3)
        {
            HalfTimeTransitionToNormal();
        }

        if (Transition4 &&  !isTransitioning && Opacity100 && TransitionType == Transitions.Transition4)
        {
            TrueTransitionToDark();
        }
        else if (!Transition4 && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition4)
        {
            TrueTransitionToNormal();
        }

        /*
        if (Transition5 && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition5)
        {
            TransitionToDarkAnimator();
        }

        if (!Transition5 && !isTransitioning && Opacity100 && TransitionType == Transitions.Transition5)
        {
            TransitionToNormalAnimator();
        }

        if (animator.GetBool("Transition") == false && animator.GetBool("Light") == false ||
            animator.GetBool("Transition") == false && animator.GetBool("Dark") == false)
        {
            ResetAnimator();
        }
        */

        #region LERP MANUAL
        /*
        if (isTransitioningLerp && Opacity100)
        {
            float elapsedTime = Time.time - startTime; // Calcula el tiempo transcurrido desde el inicio de la transici�n
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / TransitionDuration);

            if (isDarkTransition)
            {
                float opacity = Mathf.Lerp(originalOpacity, fullOpacity, t);
                image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            } 
            else
            {
                float opacity = Mathf.Lerp(fullOpacity, originalOpacity, t);
                image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            }
            // Interpola entre los valores de opacidad inicial y objetivo
            
            if (elapsedTime >= 1f || elapsedTime <= 0f)
            {
                isTransitioning = false; // Desactiva el indicador de transici�n
                isTransitioningLerp = false;
            }
            
        }
        */
        #endregion
        //EL LERP DE MANERA MANUAL CON CODIGO ES DOLOROSO NO LO SIGUIENTE

    }
    //Fin pruebas

    private void Awake()
    {
        image = _childTransform.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        Invoke("ActiveOpacity", TransitionDuration);
        animator = GetComponentInChildren<Animator>();
        _childTransform = GetComponentInChildren<Transform>();
    }

    private void ActiveOpacity()
    {
        Opacity100 = true;
        _childTransform.gameObject.SetActive(true);
        InstantTransitionToNormal();
        //print("Opacidad activada");
    }

    //PRIMERA TRANSICION
    public void TransitionToDark()
    {
        //print("Cambiando a oscuro");
        isTransitioning = true;
        image.CrossFadeAlpha(fullOpacity, TransitionDuration, false);
        Invoke("EndTransition", TransitionDuration);
    }

    public void TransitionToNormal()
    {
        //print("cambiando a normal");
        isTransitioning = true;
        image.CrossFadeAlpha(originalOpacity, TransitionDuration, false);
        Invoke("EndTransition", TransitionDuration);

    }
    //FIN PRIMERA TRANSICION

    //SEGUNDA TRANSICION
    public void InstantTransitionToNormal()
    {
        image.CrossFadeAlpha(originalOpacity, 0, false);
    }

    public void InstantTransitionToDark()
    {
        image.CrossFadeAlpha(fullOpacity,0, false);
    }
    //FIN SEGUNDA TRANSICION

    //TERCERA TRANSICION
    public void HalfTimeTransitionToNormal()
    {
        image.CrossFadeAlpha(originalOpacity, TransitionDuration / 2, false);
    }

    public void HalfTimeTransitionToDark()
    {
        image.CrossFadeAlpha(fullOpacity, TransitionDuration / 2, false);
    }
    //FIN TERCERA TRANSICION

    //CUARTA TRANSICION
    public void TrueTransitionToNormal()
    {
        image.CrossFadeAlpha(originalOpacity, TransitionDuration, true);
    }

    public void TrueTransitionToDark()
    {
        image.CrossFadeAlpha(fullOpacity, TransitionDuration, true);
    }
    //FIN CUARTA TRANSICION

    //QUINTA TRANSICION
    public void TransitionToDarkAnimator()
    {
        animator.SetBool("Transition", true);
        animator.SetBool("Dark", true);
        Invoke("ResetAnimator", 1f);
        isTransitioning = true;
    }

    public void TransitionToNormalAnimator()
    {
        animator.SetBool("Transition", true);
        animator.SetBool("Light", true);
        Invoke("ResetAnimator", 1f);
        isTransitioning = true;
    }

    public void ResetAnimator()
    {
        animator.SetBool("Transition", false);
        animator.SetBool("Dark", false);
        animator.SetBool("Light", false);
        isTransitioning = false;
    }

    //FIN QUINTA TRANSICION

    //SEXTA TRANSICION
    public void TransitionToDarkLerp()
    {
        startTime = Time.time;
        isTransitioningLerp = false;
        isDarkTransition = true;
        endTransition = false;
    }

    public void TransitionToNormalLerp()
    {
        startTime = Time.time;
        isTransitioningLerp = false;
        isDarkTransition = false;
        endTransition = false;
    }
    //FIN SEXTA TRANSICION
    private void EndTransition()
    {
        isTransitioning = false;
        //print("Ya no tengo cambios");

    }
    
}
