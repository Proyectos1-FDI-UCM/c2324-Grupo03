using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Codigo de Iker :D
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image barImage;
   
    public void UpdateHealthBar(float maxHealth, float health)
    {
        barImage.fillAmount = health / maxHealth;
    }
}
