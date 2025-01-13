using UnityEngine;

public abstract class PersistentSignleton<T> : MonoBehaviour where T : Component {
    /*
        to be inherited by all global classes - ie. manager scripts
    */
    protected static T instance;

    public static bool HasInstance => instance != null;
    public static T TryGetInstance() => HasInstance ? instance : null;

    public static T Instance {
        get { 
            return instance;
        }
    }


    /// <summary>
    /// use base.Awake() if you need to override Awake
    /// </summary>
    protected virtual void Awake() {
        InitialiseSingleton();
    }

    protected virtual void InitialiseSingleton() {
        if (!Application.isPlaying) return;

        if (instance == null) {
            instance = this as T;
        }
        if (instance != this) {
            Destroy(gameObject); 
        }
    }
}
