using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
	private static readonly ServiceLocator _instance = new ServiceLocator();

	public static ServiceLocator Instance
	{
		get { return _instance; }
	}

	private Dictionary<Type, object> _services = new Dictionary<Type, object>
	{
		{typeof(GameSettingsProvider), new GameSettingsProvider()},
		{typeof(CharactersService), new CharactersService()},
        {typeof(BackgroundsProvider), new BackgroundsProvider()},
        {typeof(DummyGenerator), new DummyGenerator()},
    };

	public T Resolve<T>() where T : class
	{
		if (_services.ContainsKey(typeof(T)))
			return _services[typeof(T)] as T;

		Debug.LogError("Register service!");
		return null;
	}
}