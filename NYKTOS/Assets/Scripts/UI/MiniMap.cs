using UnityEngine;
using UnityEngine.Rendering;

public class MiniMap : MonoBehaviour {
    //Se encarga de convertir la posicion del player para el mapa
    [Header("Transforms World")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    [Header("Transforms MiniMap")]
    [SerializeField] private RectTransform _playerMiniMap;
    [SerializeField] private RectTransform _miniMapPoint1;
    [SerializeField] private RectTransform _miniMapPoint2;

    private float miniMapRatio = 0f;

    void Awake() {
        MiniMapRatio();
    }

    // Update is called once per frame
    void Update() {
        _playerMiniMap.anchoredPosition = _miniMapPoint1.anchoredPosition + new Vector2((_playerTransform.position.x - _point1.position.x) * miniMapRatio, (_playerTransform.position.y - _point1.position.y) * miniMapRatio);
    }


    private void MiniMapRatio() {

        //Distancia en el mundo
        Vector3 vecDistancia = _point1.position - _point2.position;
        vecDistancia.y = 0f;
        float distanciaWorld = vecDistancia.magnitude;

        // distancia MiniMap
        
        float distanciaMiniMap = (_miniMapPoint1.anchoredPosition -_miniMapPoint2.anchoredPosition).magnitude;
        miniMapRatio = distanciaMiniMap / distanciaWorld;
        Debug.Log("[MINIMAP RATIO] " + miniMapRatio);


    }

}