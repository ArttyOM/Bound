using System;
using System.Collections.Generic;
using Assets;
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
	};

	public T Resolve<T>() where T : class
	{
		if (_services.ContainsKey(typeof(T)))
			return _services[typeof(T)] as T;

		Debug.LogError("Register service!");
		return null;
	}
}