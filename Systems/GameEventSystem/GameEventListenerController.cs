
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[ExecuteInEditMode]
public class GameEventListnerController : MonoBehaviour {
    [SerializeField] public List<T_GameEventListener> eventsListeners = new List<T_GameEventListener>();

    public void OnEnable() {
        foreach (T_GameEventListener listener in eventsListeners) { listener.OnEnable(); }
    }

    public void OnDisable() {
        foreach (T_GameEventListener listener in eventsListeners) { listener.OnDisable(); }
    }

    
}

[Serializable]
public struct T_GameEventListener {

    // game event to listen to
    public SO_GameEvent GameEvent;

    // response when game event is raised/fired
    public UnityEvent Response;

    public void OnEnable() {
        GameEvent.RegisterListener(this);
    }

    public void OnDisable() {
        GameEvent.UnRegisteristener(this);
    }

    public void OnEventRaised() {
        Response.Invoke();
    }
}