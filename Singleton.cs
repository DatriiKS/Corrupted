using UnityEngine;

public abstract class Singleton<T>: MonoBehaviour where T : MonoBehaviour
{
    public static bool IsQuitting { get; private set; }

    private static T _instance;

    private static object _lock = new object();
    public static T Instance
    {
        get
        {
            if (IsQuitting)
            {
                Debug.LogWarning($"Instance of{nameof(Singleton<T>)} of type {typeof(T)} cant be returned due to application quit");
                return null;
            }

            lock (_lock)
            {
                if (_instance != null)
                    return _instance;
                var instances = FindObjectsOfType<T>();
                int count = instances.Length;
                if (count > 0)
                {
                    if (count == 1)
                    {
                        DontDestroyOnLoad(instances[0]);
                        return _instance = instances[0];
                    }
                    Debug.LogError("");
                    for (int i = 1; i < instances.Length; i++)
                    {
                        Destroy(instances[i]);
                    }
                    DontDestroyOnLoad(instances[0]);
                    return _instance = instances[0];
                }
                _instance = new GameObject("Singleton" + typeof(T).ToString()).AddComponent<T>();
                DontDestroyOnLoad(_instance);
                return _instance;
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        IsQuitting = true;
        Debug.Log($"SingletonISQuittingStatus = {IsQuitting}");
    }
}
