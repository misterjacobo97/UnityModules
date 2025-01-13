using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public BaseState(EState key){
        StateKey = key;
    }

    public EState StateKey;

    public abstract void EnterState();    
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract EState GetNextState();
    public abstract void OnTriggerEnter(Collider collider);
    public abstract void OnTriggerStay(Collider collider);
    public abstract void OnTriggerExit(Collider collider);
}
