using System;
using System.Collections.Generic;
using UnityEngine;

public class SL : MonoBehaviour
{
    private Dictionary<Type, object> services = new();

    private static SL _instance;
    
    public static SL Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ServiceLocator");
                _instance = go.AddComponent<SL>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<SL>();
        }
    }

    public void RegisterService<T>(T service)
    {
        var type = typeof(T);

        if (!services.ContainsKey(type))
        {
            services.Add(type, service);
        }
    }

    public T GetService<T>()
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            return (T)services[type];
        }
        else
        {
            Debug.LogError("Такого сервиса не имеется(");
            return default;
        }

    }
    
}
