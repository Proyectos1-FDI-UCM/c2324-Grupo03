using UnityEngine;

// No se usa (mirar DefenseDeath en su lugar)
public class BuildingDeath : MonoBehaviour, IDeath
{    public void Death()
    {
        BuildingManager.Instance.RemoveBuilding(this.gameObject);
        Destroy(gameObject);
    }
}
