using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateManager<EState> : MonoBehaviour where EState : Enum {

    [SerializeField] protected List<BaseState<EState>> States = new();
    protected BaseState<EState> CurrentState;
    [SerializeField] protected EState InitialState;
    protected ScriptableObject _context;

    public void Init(ScriptableObject context) {
        _context = context;
    }

    protected virtual void Start(){
        //foreach (BaseState state in States) {
        //    state.Init(_context);
        //}
        // gotta do this in the derived state machine

        CurrentState = States.Find(s => s.StateKey.Equals(InitialState));

        CurrentState.EnterState();
    }

    protected virtual void Update(){
        EState nextStateKey = CurrentState.GetNextState();

        if (nextStateKey.Equals(CurrentState.StateKey)){
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
        CurrentState.ExitState();
        CurrentState = States.Find(s => s.StateKey.Equals(stateKey));
        CurrentState.EnterState();
    }
    

}
