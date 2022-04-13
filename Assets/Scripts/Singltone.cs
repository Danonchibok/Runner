using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singltone<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    public virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}
