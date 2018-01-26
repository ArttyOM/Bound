using UnityEngine;

namespace Assets
{
	public class GameSettingsProvider
	{
		private string path = "Settings";
		private GameSettings _gameSettings;
		
		public GameSettings GetSettings()
		{
			return _gameSettings ?? (_gameSettings = Resources.Load<GameSettings>(path));
		}
	}
}