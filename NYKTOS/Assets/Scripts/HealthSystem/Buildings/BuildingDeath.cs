using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDeath : MonoBehaviour, IDeath
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Death()
    {
        BuildingManager.Instance.RemoveBuilding(this.gameObject);
        Destroy(gameObject);
    }
}
