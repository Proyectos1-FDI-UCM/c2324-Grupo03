using UnityEngine;

/// <summary>
/// La función de esta clase es ir lanzando las waves poco a poco
/// según el tiempo de cada wave
/// 
/// <para>
/// Esta clase es un singleton
/// </para>
/// </summary>
public class NightManager : MonoBehaviour
{
    // Noche en curso
    private NightWave _currentNightData;

    private int _currentWaveNumber;

    // Tracker de progreso de la partida, lo suyo sería que esto fuera un ScriptableSingleton global
    // Aunque de esta forma se podrían considerear diferentes "niveles"
    [SerializeField]
    private NightProgressTracker _progressTracker;

    // Contenedor donde se guardan los datos de los cristales a dropear, debería ser un singleton global
    [SerializeField]
    private CrystalDrops _dropTracker;

    // Evento de inicio de spawneo
    [SerializeField]
    private SpawndataEmitter _spawnerEmitter;

    static private NightManager _instance;

    /// <summary>
    /// Inicializa la noche proveida, si contiene alguna wave inicializa la primera wave
    /// 
    /// <para>
    /// Se llamará al método EndNight cuando pasen los segundos de duración de la noche
    /// </para>
    /// </summary>
    /// <param name="night"> Datos de la noche a inicializar </param>
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

    /// <summary>
    /// Inicializa la wave en _currentWaveNumber lanzando el evento de spawneo.
    /// 
    /// <para>
    /// Si hay una wave después de esta se lanzará después del tiempo de la actual.
    /// </para>
    /// </summary>
    private void InitializeWave()
    {
        Wave currentWave = _currentNightData.WaveList[_currentWaveNumber];

        currentWave.WaveValidate();

        foreach(var item in currentWave.WaveData)
        {
            Debug.Log("DEBUG NIGHT WAVE: " + item.Key);
        }

        _spawnerEmitter.InvokePerform(currentWave.WaveData);

        Debug.Log("[NightManager] Inicializada wave - " + _currentWaveNumber);

        if(_currentWaveNumber+1 < _currentNightData.WaveList.Length)     
        {
            Invoke(nameof(AdvanceWave), currentWave.time);
        }
    }

    /// <summary>
    /// Avanzar la wave en 1 e inicializarla
    /// </summary>
    private void AdvanceWave()
    {
        Debug.Log("[NightManager] Se lanza una wave");
        _currentWaveNumber ++;
        InitializeWave();    
    }

    /// <summary>
    /// Se llama al fin de la noche en el progress tracker
    /// </summary>
    public void EndNight()
    {
        _progressTracker.AdvanceNight();
        
        // pasar a estado de día
        Debug.Log("[NightManager] Se completó la noche");
    }

    /// <summary>
    /// Aplicación de singletón
    /// </summary>
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

    void OnDestroy()
    {
        _progressTracker.StartNight.RemoveListener(StartNight);
    }
}