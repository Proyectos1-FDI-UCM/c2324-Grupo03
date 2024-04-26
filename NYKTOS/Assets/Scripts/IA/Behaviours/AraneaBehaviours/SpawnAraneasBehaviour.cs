using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que hereda de IBehaviour y es un behaviour de la maquina de estados de la aranea, se encarga de spawnear las Araneas Hijas
/// </summary>
public class SpawnAraneasBehaviour : MonoBehaviour, IBehaviour 
{
	  #region references
	  [SerializeField] 
    private GameObject _araneaHijaPrefab;

    private Transform _araneaMadreTransform;
	  #endregion

	  #region parameters
	  [SerializeField] 
    private int _maxHijas = 3;

    private int _currentHijas;
	  #endregion

	  /// <summary>
	  /// M�todo heredado de IBehaviour que se encarga de llamar el m�todo SpawnSpider si aun no se ha sobrepasado el n�umero de hijas maximo
	  /// </summary>
	  public void PerformBehaviour() 
    {
        if (_currentHijas < _maxHijas)
        {
            _currentHijas++;
            SpawnSpider();
        }

    }

    /// <summary>
    /// M�todo que se encarga de spawnear las Araneas Hijas
    /// </summary>
    private void SpawnSpider() 
    {
        if (_araneaHijaPrefab != null && _araneaMadreTransform != null) Instantiate(_araneaHijaPrefab, _araneaMadreTransform.position, Quaternion.identity);
        else Debug.LogWarning("No hay prefab");
        
    }

    private void Awake() 
    {
        _araneaMadreTransform = GetComponentInParent<Transform>();
    }
}
