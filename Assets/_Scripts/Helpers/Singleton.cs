using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    static T instance;

    public static T Instance
    {
        get 
        {
            try
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null) throw new NullReferenceException();
            }
            
            catch (NullReferenceException e)
            {
                Debug.Log(e.Message);
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);
            }
           
            finally
            {
                Debug.Log("This Code Always Runs");
            }

            return instance;
        }

    }
   protected virtual void Awake()
    {
        if (!instance)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
