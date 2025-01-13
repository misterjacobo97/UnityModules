using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class BaseStateManager<EState> : MonoBehaviour where EState : Enum
{
    [SerializedDictionary] public Dictionary<EState, BaseState<EState>> States = new();
    protected BaseState<EState> CurrentState;
    protected EState InitialState;

    // protected bool isTransitioningState = false;

    void Awake(){}
    void Start(){
        CurrentState.EnterState();
    }

    void Update(){
        EState nextStateKey = CurrentState.GetNextState();

        if (/*!isTransitioningState &&*/ nextStateKey.Equals(CurrentState.StateKey)){
            CurrentState.UpdateState();
        } else {
            TransitionToState(nextStateKey);
        }
    }

    void FixedUpdate(){
        CurrentState.FixedUpdateState();
    }

    void OnTriggerEnter(Collider collider){
        CurrentState.OnTriggerEnter(collider);
    }
    void OnTriggerStay(Collider collider){
        CurrentState.OnTriggerStay(collider);
    }
    void OnTriggerExit(Collider collider){
        CurrentState.OnTriggerExit(collider);
    }

    void TransitionToState(EState stateKey){
        // isTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        // isTransitioningState = false;
    }
    

}
