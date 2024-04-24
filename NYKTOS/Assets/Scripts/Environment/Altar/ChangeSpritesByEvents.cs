using UnityEngine;
/// <summary>
/// Clase responsable de cambiar los sprites de los altares en función de su estado
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ChangeSpritesAltares : MonoBehaviour
{
	#region references
	[SerializeField]
	private VoidEmitter _placeHolderSprite;

	private BuildingStateMachine _state;
	private AltarComponent _altarComponent;
	private SpriteRenderer _currentSprite;

	#region sprites
	[Header("Sprites")]
	[Space(10)]
	[Header("    Destruido")]
	[SerializeField]
	private Sprite _sprite0;

	[Space(10)]
	[Header("    Casi Destruido")]
	[SerializeField]
	private Sprite _sprite1;

	[Space(10)]
	[Header("    Medio Construido")]
	[SerializeField]
	private Sprite _sprite2;

	[Space(10)]
	[Header("    Casi Construido")]
	[SerializeField]
	private Sprite _sprite3;

	[Space(10)]
	[Header("    Construido")]
	[SerializeField]
	private Sprite _sprite4;
	#endregion

	#endregion

	void Start()
	{
		_currentSprite = GetComponent<SpriteRenderer>();
		_altarComponent = GetComponentInParent<AltarComponent>();
		_placeHolderSprite.Perform.AddListener(ChangeSprite);
		_state = GetComponentInParent<BuildingStateMachine>();

		ChangeSprite();
	}

	/// <summary>
	/// Método responsable de cambiar los sprites de los altares
	/// </summary>
	private void ChangeSprite()
	{
		if (_state.buildingState == BuildingStateMachine.BuildingState.Built)
		{
			_currentSprite.sprite = _sprite4;
		}
		else
		{
			switch (_altarComponent.CurrentPlaceholders)
			{
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
	/// <summary>
	/// Método para eliminar el listener
	/// </summary>
	private void OnDestroy()
	{
		_placeHolderSprite.Perform.RemoveListener(ChangeSprite);
	}
}
