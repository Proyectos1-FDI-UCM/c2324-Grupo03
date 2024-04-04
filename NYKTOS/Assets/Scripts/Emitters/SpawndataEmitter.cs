using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Spawndata Emitter", menuName = "Emitter/Spawndata")]
public class SpawndataEmitter  : GenericEmitter<Dictionary<SpawnerRegion, Enemy[]>> {}