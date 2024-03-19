using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

//Codigo de Iker :D
public class TransitionController : MonoBehaviour
{
    private Image image;
    private Animator animator;

    [SerializeField]
    private float TransitionDuration = 1.5f;
    private float originalOpacity = 0f;
    private float fullOpacity = 1f;
    private float startTime;
    
    
    //Para pruebas
    public bool Transition = false;
    private bool isTransitioning = false;
    private bool isTransitioningLerp = false;
    private bool isDarkTransition = false;
    private bool Opacity100 = false;
    private bool endTransition = false;
    private bool NormalState = false;

    private void Update()
    {
        if (Transition && !isTransitioning && Opacity100)
        {
            TransitionToDark();
            //TransitionToDarkInstant();
            //TransitionToDarkLerp();


        }
        else if (!Transition && !isTransitioning && Opacity100)
        {
            TransitionToNormal();
            //TransitionToNormalInstant();
            //TransitionToNormalLerp();
        }

        /*
        if (animator.GetBool("Transition") == true && animator.GetBool("Dark") == true)
        {
            TransitionToDarkAnimator();
        }

        if (animator.GetBool("Transition") == true && animator.GetBool("Light") == true)
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
            float elapsedTime = Time.time - startTime; // Calcula el tiempo transcurrido desde el inicio de la transición
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
                isTransitioning = false; // Desactiva el indicador de transición
                isTransitioningLerp = false;
            }
            
        }
        */
        #endregion
        //EL LERP DE MANERA MANUAL CON CODIGO ES DOLOROSO NO LO SIGUIENTE

    }
    //Fin pruebas

    private void Start()
    {
        image = GetComponent<Image>();
        Invoke("ActiveOpacity", TransitionDuration);
        animator = GetComponent<Animator>();
    }

    private void ActiveOpacity()
    {
        Color color = image.color;
        color.a = fullOpacity;
        image.color = color;
        Opacity100 = true;
        //image.gameObject.SetActive(true);
        print("Opacidad activada");
    }

    //PRIMERA TRANSICION

    public void TransitionToDark()
    {
        print("Cambiando a oscuro");
        isTransitioning = true;
        image.CrossFadeAlpha(fullOpacity, TransitionDuration, false);
        Invoke("EndTransition", TransitionDuration);

    }

    public void TransitionToNormal()
    {
        print("cambiando a normal");
        isTransitioning = true;
        image.CrossFadeAlpha(originalOpacity, TransitionDuration, false);
        Invoke("EndTransition", TransitionDuration);

    }
    //FIN PRIMERA TRANSICION

    //SEGUNDA TRANSICION
    public void TransitionToDarkInstant()
    {
        float opacity = Mathf.Lerp(originalOpacity, fullOpacity, TransitionDuration);
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }

    public void TransitionToNormalInstant()
    {
        float opacity = Mathf.Lerp(fullOpacity, originalOpacity, TransitionDuration);
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }
    //FIN SEGUNDA TRANSICION

    //TERCERA TRANSICION
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
    //FIN TERCERA TRANSICION

    //CUARTA TRANSICION
    /*
    public void TransitionToDarkAnimator()
    {
        animator.SetBool("Transition", true);
        animator.SetBool("Dark", true);
        Invoke("ResetAnimator", 1f);
    }

    public void TransitionToNormalAnimator()
    {
        animator.SetBool("Transition", true);
        animator.SetBool("Light", true);
        Invoke("ResetAnimator", 1f);
    }

    public void ResetAnimator()
    {
        animator.SetBool("Transition", false);
        animator.SetBool("Dark", false);
        animator.SetBool("Light", false);
    }
    */
    //FIN CUARTA TRANSICION

    private void EndTransition()
    {
        isTransitioning = false;
        print("Ya no tengo cambios");

    }
    
}
