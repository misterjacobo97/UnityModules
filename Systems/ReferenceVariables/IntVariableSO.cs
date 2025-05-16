using R3;
using UnityEngine;

[CreateAssetMenu (fileName ="IntVariable", menuName ="Primitives/IntVariable")]
public class IntVariable : ScriptableObject {
    public enum RESET_TYPE {
        NONE,
        MIN,
        MAX,
        MID
    }
    public RESET_TYPE resetType = RESET_TYPE.NONE;

    public int maxValue;
    public int minValue;

    public SerializableReactiveProperty<int> reactiveValue;

    private void OnEnable () {
        reactiveValue = new SerializableReactiveProperty<int>();

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
