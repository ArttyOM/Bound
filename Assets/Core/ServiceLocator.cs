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
		{typeof(GameObjectsProvider), new GameObjectsProvider()},
        {typeof(BackgroundsProvider), new BackgroundsProvider()},
        {typeof(DummyGenerator), new DummyGenerator()},
    };

	private Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
	
	public T ResolveService<T>() where T : class
	{
		if (_services.ContainsKey(typeof(T)))
			return _services[typeof(T)] as T;

		Debug.LogError("Register service!");
		return null;
	}

	public T ResolveSingleton<T>() where T : class
	{
		if (_singletons.ContainsKey(typeof(T)))
			return _singletons[typeof(T)] as T;

		Debug.LogError("Register singleton!");
		return null;
	}
	
	public void RegisterSingleton(object instance)
	{
		var type = instance.GetType();
		if (_singletons.ContainsKey(type))
		{
			Debug.LogError("Already registered");
			return;
		}

		_singletons.Add(type, instance);
		Debug.Log("Singleton " + type.Name + " registered");
	}
}