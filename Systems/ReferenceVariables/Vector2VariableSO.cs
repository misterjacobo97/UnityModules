using R3;
using UnityEngine;

[CreateAssetMenu (fileName ="Vector2Variable", menuName ="Primitives/Vector2Variable")]
public class Vector2Variable : ScriptableObject {
    public enum RESET_TYPE {
        NONE,
        ZERO
    }
    public RESET_TYPE resetType = RESET_TYPE.ZERO;

    public float maxXMagnitude = -1;
    public float maxYMagnitude = -1;
    public float maxVectorLength = -1; 

    public SerializableReactiveProperty<Vector2> reactiveValue;

    private void OnEnable () {
        reactiveValue = new SerializableReactiveProperty<Vector2>();

        switch (resetType) {
            case RESET_TYPE.ZERO:
                reactiveValue.Value = Vector2.zero;
                break;
        }

    }

    private void OnDisable () {
        reactiveValue?.Dispose();
    }
}
