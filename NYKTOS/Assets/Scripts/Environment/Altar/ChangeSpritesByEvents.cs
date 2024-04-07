using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class ChangeSpritesByEvents : MonoBehaviour
{

    [SerializeField] private VoidEmitter _placeHolderSprite;

    private BuildingStateMachine _state;
    private AltarComponent _altarComponent;
    private SpriteRenderer _currentSprite;

    [SerializeField]
    private Sprite _sprite0;
    [SerializeField]
    private Sprite _sprite1;
    [SerializeField]
    private Sprite _sprite2;
    [SerializeField]
    private Sprite _sprite3;
    [SerializeField]
    private Sprite _sprite4;

    void Start()
    {
        _currentSprite = GetComponent<SpriteRenderer>();

        _altarComponent = GetComponentInParent<AltarComponent>();
        _placeHolderSprite.Perform.AddListener(ChangeSprite);
        _state = GetComponentInParent<BuildingStateMachine>();

        ChangeSprite();
    }

    private void ChangeSprite() 
    {
        if (_state.buildingState == BuildingStateMachine.BuildingState.Built) {
            _currentSprite.sprite = _sprite4;
        } else{

            switch (_altarComponent.CurrentPlaceholders) {
                case 0:
                    _currentSprite.sprite = _sprite0;
                    break;
                case 1:
                    _currentSprite.sprite = _sprite1;
                    break;
                case 2:
                    _currentSprite.sprite = _sprite2;
                    break;
                case 3:
                    _currentSprite.sprite = _sprite3;
                    break;
            }

        }
    }

    private void OnDestroy() {
        _placeHolderSprite.Perform.RemoveListener(ChangeSprite);
    }
}
