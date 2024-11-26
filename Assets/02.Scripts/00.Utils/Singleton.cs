using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    private T instance;
    public T Instance
    {
        get 
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }
        }
    }


}
