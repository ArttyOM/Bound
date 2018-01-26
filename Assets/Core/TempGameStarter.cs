using UnityEngine;

public class TempGameStarter : MonoBehaviour
{
	private void Start()
	{
		var charactersService = ServiceLocator.Instance.Resolve<CharactersService>();
		
		var wizard = Instantiate(charactersService.GetCharacterPrefab<Wizard>());
		var warrior = Instantiate(charactersService.GetCharacterPrefab<Warrior>());
	}
}
