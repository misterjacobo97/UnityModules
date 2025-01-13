using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventObject", menuName = "Systems/GameEventObject")]
public class SO_GameEvent : ScriptableObject {
    // list of listeners that this event will notify if it is invoked
    List<T_GameEventListener> listeners = new List<T_GameEventListener>();

    public void RegisterListener(T_GameEventListener listener) {
        listeners.Add(listener);
    }

    public void UnRegisteristener(T_GameEventListener listener) {
        listeners.Remove(listener);
    }

    public void RaiseEvent() {
        foreach (T_GameEventListener listener in listeners) {
            listener.OnEventRaised();
        }
    }
}
