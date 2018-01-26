using Assets.Core.Characters;
using UnityEngine;

public class TempGameStarter : MonoBehaviour
{
	private void Start()
	{
		var gameObjectsResolverService = ServiceLocator.Instance.Resolve<GameObjectsResolver>();
		
		var wizard = Instantiate(gameObjectsResolverService.GetCharacterPrefab<Wizard>())
			.GetComponent<Character>();
		var warrior = Instantiate(gameObjectsResolverService.GetCharacterPrefab<Warrior>())
			.GetComponent<Character>();
		var transmission = Instantiate(gameObjectsResolverService.GetCharacterPrefab<Transmission>())
			.GetComponent<Transmission>();
		
		transmission.SetCharacters(wizard, warrior);
	}
}
