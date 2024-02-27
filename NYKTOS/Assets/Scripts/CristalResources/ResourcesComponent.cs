using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesComponent : MonoBehaviour
{
    //Recoge los drops de los recursos y recopila el numero de cristales amarillos, azules y magentas que tienes
    //Para interactuar con los edificios, necesitas una cantidad de cristales amarillos
    //Para interactuar con las armas, necesitas una cantidad de cristales amarillos, azules y/o magentas

    [SerializeField]
    private int _yellowCrystal = 0;
    [SerializeField]
    private int _blueCrystal = 0;
    [SerializeField]
    private int _magentaCrystal = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
