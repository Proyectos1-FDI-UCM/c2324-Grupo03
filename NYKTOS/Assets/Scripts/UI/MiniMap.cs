using UnityEngine;
using UnityEngine.Rendering;

public class MiniMap : MonoBehaviour
{
	//Se encarga de convertir la posicion del player para el mapa
	[Header("Transforms World")]
	[SerializeField] private Transform _playerTransform;
	[SerializeField] private Transform _point1;
	[SerializeField] private Transform _point2;

	[Header ("Transforms MiniMap")]
	[SerializeField] private RectTransform _playerTransformRect;
	[SerializeField] private RectTransform _point1Rect;
	[SerializeField] private RectTransform _point2Rect;

	private float miniMapRatio =0f;

	void Awake()
	{
		MiniMapRatio();
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(_playerTransform.position.x);
		_playerTransformRect.anchoredPosition = _point1Rect.anchoredPosition + new Vector2((_playerTransform.position.x - _point1.position.x) *miniMapRatio , (_playerTransform.position.y - _point1.position.y)*miniMapRatio);
	}


	private void MiniMapRatio()
	{
		//Distancia en el mundo
		Vector3 vecDistancia = _point1.position - _point2.position;
		//vecDistancia.y = 0f;
		float distanciaWorld = vecDistancia.magnitude;
		/*distanciaWorld = Mathf.Sqrt(
			Mathf.Pow(_point1.position.x - _point2.position.x, 2) +
			Mathf.Pow(_point1.position.y - _point2.position.y, 2));*/


		//Distancia en el MiniMap

		Vector3 vecdistanciaMiniMap = _point1Rect.anchoredPosition - _point2Rect.anchoredPosition;
		float distanciaMiniMap = Mathf.Sqrt(
			Mathf.Pow(_point1Rect.anchoredPosition.x - _point2Rect.anchoredPosition.x, 2) +
			Mathf.Pow(_point1Rect.anchoredPosition.y - _point2Rect.anchoredPosition.y, 2));

		miniMapRatio = distanciaWorld - distanciaMiniMap;
		Debug.Log(miniMapRatio);


	}

}
