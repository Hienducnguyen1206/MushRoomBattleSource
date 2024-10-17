using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

   

    public static T Instance
    {
        get
        {
            

            lock (_lock)
            {
                if (_instance == null)
                {
                   
                    _instance = FindObjectOfType<T>(true);

                    if (_instance == null)
                    {
                        
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();
                       // DontDestroyOnLoad(singletonObject);
                        Debug.Log($"[Singleton] An instance of {typeof(T)} was created with DontDestroyOnLoad.");
                    }
                }

                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        
        if (_instance == null)
        {
            _instance = this as T;
           // DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T)} detected. Destroying new instance.");
            Destroy(gameObject); 
        }
    }

    protected virtual void OnDestroy()
    {
       
        if (_instance == this)
        {
            _instance = null;
        }
    }

   
}
