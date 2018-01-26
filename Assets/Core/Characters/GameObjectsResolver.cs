using System;
using System.Collections.Generic;
using Assets.Core.Characters;
using UnityEngine;

public class GameObjectsResolver
{
	private Dictionary<Type, string> _characterPathMapper = new Dictionary<Type, string>
	{
		{typeof(Wizard), "Characters/Wizard"},
		{typeof(Warrior), "Characters/Warrior"},
		{typeof(Transmission), "Characters/Transmission"},
	};

	public GameObject GetCharacterPrefab<T>() where T : class
	{
		string path;
		if (_characterPathMapper.TryGetValue(typeof(T), out path))
			return Resources.Load<GameObject>(path);

		Debug.LogError("No path for such character!");
		return null;
	}
}