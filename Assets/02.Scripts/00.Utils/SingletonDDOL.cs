using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDDOL<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                instance?.GetComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
