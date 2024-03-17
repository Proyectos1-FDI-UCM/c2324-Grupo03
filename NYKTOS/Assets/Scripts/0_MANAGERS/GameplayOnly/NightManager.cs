using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private int maxNights = 3;

    [SerializeField]
    private SaveData _saveData;

    #region parameters

    [SerializeField]
    private int _nightLength = 180;

    #endregion

    private static NightManager _instance;
    public static NightManager Instance
    {
        get { return _instance; }
    }

    public void StartNight()
    {
        GameManager.Instance.UpdateGameState(GameState.Night);
        Invoke(nameof(EndNight), _nightLength);
        
        // Aquí se debería inicializar el reloj

    }

    public void EndNight()
    {
        GameManager.Instance.UpdateGameState(GameState.Day);

        _saveData.AdvanceNight();
    }

    void Awake()
    {
        if (_instance != null) 
            Destroy(gameObject);
        else 
            _instance = this;
    }
}
