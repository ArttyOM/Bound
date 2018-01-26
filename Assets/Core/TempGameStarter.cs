using Assets.Core.Characters;
using UnityEngine;

public class TempGameStarter : MonoBehaviour
{
	private void Start()
	{
		var gameObjectsResolverService = ServiceLocator.Instance.Resolve<GameObjectsProvider>();
		var settings = ServiceLocator.Instance.Resolve<GameSettingsProvider>().GetSettings();

//		var wizard = Instantiate(gameObjectsResolverService.GetCharacterPrefab<Wizard>())
//			.GetComponent<Character>();
//		var warrior = Instantiate(gameObjectsResolverService.GetCharacterPrefab<Warrior>())
//			.GetComponent<Character>();
//		var transmission = Instantiate(gameObjectsResolverService.GetCharacterPrefab<Transmission>())
//			.GetComponent<Transmission>();
//		var camera = Instantiate(gameObjectsResolverService.GetCharacterPrefab<CameraController>())
//			.GetComponent<CameraController>();

//		transmission.SetCharacters(wizard, warrior);
//		camera.SetCharacters(wizard, warrior);
//		camera.SetHeight(settings.CameraHeight);
//		wizard.Controller.SetMinMaxDistances(settings.TransmissionMinLength, settings.TransmissionMaxLength);
//		warrior.Controller.SetMinMaxDistances(settings.TransmissionMinLength, settings.TransmissionMaxLength);
	}
}