using R3;
using UnityEngine;

[CreateAssetMenu (menuName ="Primitives/BoolVariable")]
public class BoolVariable : ScriptableObject {

        public enum RESET_TYPE {
        NONE,
        TRUE,
        FALSE
    }
    public RESET_TYPE resetType = RESET_TYPE.NONE;
    public SerializableReactiveProperty<bool> Value;

    public void Toggle(){
        Value.Value = !Value.Value;
    }

    private void OnEnable(){
        Value = new SerializableReactiveProperty<bool>();

        switch (resetType){
            case RESET_TYPE.TRUE:
                Value.Value = true;
                break;
            case RESET_TYPE.FALSE:
                Value.Value = false;
                break;
        }
    }

    private void OnDisable(){
        Value.Dispose();
    }
}
