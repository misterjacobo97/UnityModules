using System;
using R3;
using UnityEngine;

[Serializable]
public class FloatReference {

    public bool useConstant = false;
    public float constantValue;
    public FloatVariable variable;

    public void Reset() {
        switch (variable.resetType) {
            case FloatVariable.RESET_TYPE.MIN:
                variable.Value = variable.minValue;
                return;
            case FloatVariable.RESET_TYPE.MAX:
                variable.Value = variable.maxValue;
                return;
            case FloatVariable.RESET_TYPE.MID:
                    variable.Value = variable.maxValue / 2;
                    return;
        }
    }

    public ReactiveProperty<float> GetReactiveValue => variable.reactiveValue;
    public void AddToReactiveValue(float newVal){
        variable.Value += newVal;
    }
    public void SetReactiveValue(float newVal){
        variable.Value = newVal;
    }
}

[Serializable]
public class IntReference {

    public bool useConstant = false;
    public float constantValue;
    public IntVariable variable;

        public void Reset() {
        switch (variable.resetType) {
            case IntVariable.RESET_TYPE.MIN:
                variable.reactiveValue.Value = variable.minValue;
                return;
            case IntVariable.RESET_TYPE.MAX:
                variable.reactiveValue.Value = variable.maxValue;
                return;
        }
    }

    public ReactiveProperty<int> GetReactiveValue => variable.reactiveValue;
    public void AddToReactiveValue(int newVal){
        if (newVal > 0 && variable.maxValue != -1 && variable.reactiveValue.Value + newVal > variable.maxValue && variable.reactiveValue.Value != variable.maxValue ) {
            variable.reactiveValue.Value = variable.maxValue;
        }
        else if (newVal < 0 && variable.reactiveValue.Value + newVal < variable.minValue && variable.reactiveValue.Value != variable.minValue) {
            variable.reactiveValue.Value = variable.minValue;
        }
        else variable.reactiveValue.Value += newVal;
    }
    public void SetReactiveValue(int newVal){
        variable.reactiveValue.Value = newVal;
    }
    
}

[Serializable]
public class BoolReference {

    public BoolVariable variable;

    public ReactiveProperty<bool> GetReactiveValue => variable.Value;
    public void SetReactiveValue(bool newValue) {
        variable.Value.Value = newValue;
    }
}

[Serializable]
public class Vector2Reference {

    public Vector2Variable variable;

    public void Reset() {
        switch (variable.resetType) {
            case Vector2Variable.RESET_TYPE.ZERO:
                variable.reactiveValue.Value = Vector2.zero;
                break;
        }
    }

    public ReactiveProperty<Vector2> GetReactiveValue => variable.reactiveValue;
    public void AddToReactiveValue(Vector2 newVal){
        Vector2 newVec = Vector2.zero;

        // check for max X axis
        newVec.x = variable.maxXMagnitude != -1 
            ? Mathf.Clamp(newVal.x + variable.reactiveValue.Value.x, -variable.maxXMagnitude, variable.maxXMagnitude) 
            : newVal.x + variable.reactiveValue.Value.x;

        // check for max Y axis
        newVec.y = variable.maxYMagnitude != -1 
            ? Mathf.Clamp(newVal.y + variable.reactiveValue.Value.y, -variable.maxYMagnitude, variable.maxYMagnitude)
            : newVal.y + variable.reactiveValue.Value.y;
        
        // check for max magnitude / length and set
        variable.reactiveValue.Value = variable.maxVectorLength != -1 
            ? Vector2.ClampMagnitude(newVec, variable.maxVectorLength)
            : newVec;
    }

    public void SetReactiveValue(Vector2 newVal){
        Vector2 newVec = variable.reactiveValue.Value;

        // check for max X axis
        newVec.x = variable.maxXMagnitude != -1 
            ? Mathf.Clamp(newVal.x, -variable.maxXMagnitude, variable.maxXMagnitude) 
            : newVal.x;

        // check for max Y axis
        newVec.y = variable.maxYMagnitude != -1 
            ? Mathf.Clamp(newVal.y, -variable.maxYMagnitude, variable.maxYMagnitude)
            : newVal.y;
        
        // check for max magnitude / length and set
        variable.reactiveValue.Value = variable.maxVectorLength != -1 
            ? Vector2.ClampMagnitude(newVec, variable.maxVectorLength)
            : newVec;
    }
    
}
