using R3;
using UnityEngine;

[CreateAssetMenu (fileName ="FloatVariable", menuName ="Primitives/FloatVariable")]
public class FloatVariable : ScriptableObject {
    public enum RESET_TYPE {
        NONE,
        MIN,
        MAX,
        MID
    }
    public RESET_TYPE resetType = RESET_TYPE.NONE;

    public bool clampMaxValue = false;
    public float maxValue;
    public bool clampMinValue = false;
    public float minValue;

    public SerializableReactiveProperty<float> reactiveValue;
    public float Value {
        get {
            return reactiveValue.Value;
        }
        set {
            if (clampMaxValue && (reactiveValue.Value + value) > maxValue) {
                reactiveValue.Value = value;
            }
            else if ( clampMinValue && reactiveValue.Value + value < minValue){
                reactiveValue.Value = minValue;
            }
            else reactiveValue.Value = value;
        }
    } 

    private void OnEnable () {
        reactiveValue = new SerializableReactiveProperty<float>();

        switch (resetType) {
            case RESET_TYPE.MIN:
                reactiveValue.Value = minValue;
                break;
            case RESET_TYPE.MAX:
                reactiveValue.Value = maxValue;
                break;
            case RESET_TYPE.MID:
                reactiveValue.Value = maxValue / 2;
                break;
        }

        
    }

    private void OnDisable () {
        reactiveValue?.Dispose();
    }

}
