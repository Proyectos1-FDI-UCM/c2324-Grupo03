using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    private NightWave _currentNightData;
    private int _currentWaveNumber;

    [SerializeField]
    private NightProgressTracker _progressTracker;
    [SerializeField]
    private CrystalDrops _dropTracker;
    [SerializeField]
    private SpawndataEmitter _spawnerEmitter;

    static private NightManager _instance;

    public void StartNight(NightWave night)
    {
        Debug.Log("Nightmanager: STARTNIGHT");
        _currentNightData = night;

        Invoke(nameof(EndNight), _currentNightData.NightLength);
        _currentWaveNumber = 0;

        // [Nota para Marco, de parte de Marco del pasado]
        // Probar si puedo hacer una copia de los datos de la instancia y asignarla directamente
        _dropTracker.RequiredYellow = _currentNightData.NightCrystalDrops.RequiredYellow;
        _dropTracker.RequiredCyan = _currentNightData.NightCrystalDrops.RequiredCyan;
        _dropTracker.RequiredMagenta = _currentNightData.NightCrystalDrops.RequiredMagenta;
        _dropTracker.ProbabilityYellow = _currentNightData.NightCrystalDrops.ProbabilityYellow;
        _dropTracker.ProbabilityCyan = _currentNightData.NightCrystalDrops.ProbabilityCyan;
        _dropTracker.ProbabilityMagenta = _currentNightData.NightCrystalDrops.ProbabilityMagenta;
        

        if(_currentNightData.WaveList.Length > 0)
        {
            InitializeWave();
        }
    }

    void InitializeWave()
    {

        Wave currentWave = _currentNightData.WaveList[_currentWaveNumber];

        currentWave.WaveValidate();

        foreach(var item in currentWave.WaveData)
        {
            Debug.Log(item.Key);
        }

        _spawnerEmitter.InvokePerform(currentWave.WaveData);

        if(_currentWaveNumber+1 < _currentNightData.WaveList.Length)     
        {
            Invoke(nameof(AdvanceWave), currentWave.time);
        }
    }

    void AdvanceWave()
    {
        Debug.Log("[NightManager] Se lanza una wave");
        _currentWaveNumber ++;
        InitializeWave();    
    }

    public void EndNight()
    {
        _progressTracker.AdvanceNight();
        
        // pasar a estado de día
        Debug.Log("[NightManager] Se completó la noche");
    }

    // Aplicación de singletón
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        _progressTracker.StartNight.AddListener(StartNight);
    }

    void Start()
    {
    }

    void OnDestroy()
    {
        _progressTracker.StartNight.RemoveListener(StartNight);
    }
}