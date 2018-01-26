using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform _cachedCameraTransform;

	private Transform _firstCharacterCachedTransform;

	private Transform _secondCharacterCachedTransform;

	private float _height;

	private void Awake()
	{
		_cachedCameraTransform = transform;
	}

	public void SetCharacters(Character first, Character second)
	{
		_firstCharacterCachedTransform = first.transform;
		_secondCharacterCachedTransform = second.transform;
	}

	public void SetHeight(float height)
	{
		_height = height;
	}

	public void Update()
	{
		var targetPosition = Vector3.Lerp(_firstCharacterCachedTransform.position, _secondCharacterCachedTransform.position,
			0.5f);
		targetPosition.z = _height;
		_cachedCameraTransform.position = targetPosition;
	}
}