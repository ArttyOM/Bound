using UnityEngine;

namespace Assets.Core.Characters
{
	public class Transmission: MonoBehaviour //TODO: абстрагировать
	{
		[SerializeField] private LineRenderer _lineRenderer;

		private Transform _firstCharacterCachedTransform;
		
		private Transform _secondCharacterCachedTransform;
		
		public void SetCharacters(Character first, Character second)
		{
			_firstCharacterCachedTransform = first.transform;
			_secondCharacterCachedTransform = second.transform;
		}

		public void Update()
		{
			_lineRenderer.SetPositions(new[]
			{
				_firstCharacterCachedTransform.position,
				_secondCharacterCachedTransform.position,
			});
		}
	}
}