using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Codigo de Iker
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Se obtiene la imagen de la barra de vida rellena
    /// </summary>
    [SerializeField]
    private Image barImage;

    /// <summary>
    /// Se puede adaptar a todo, enemigos, edificios, lo unico que hace falta es pasar los parametros de vida actual y la vida completa para hacer el calculo
    /// Al hacer ese calculo se mostrará la cantidad de vida que tiene el elemento
    /// Se aprovecha en el healthcomponent para los edificios
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="health"></param>
    public void UpdateHealthBar(float maxHealth, float health)
    {
        barImage.fillAmount = health / maxHealth;
    }
}
