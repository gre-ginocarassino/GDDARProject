using UnityEngine;
using System.Collections;


public class Singleton<TInstance> : MonoBehaviour where TInstance : Singleton<TInstance>
{
    public static TInstance Instance;
    public bool isPersistant;


    public virtual void Awake()
    {
        if (isPersistant)
        {
            if (!Instance)
            {
                Instance = this as TInstance;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this as TInstance;
        }
    }
}