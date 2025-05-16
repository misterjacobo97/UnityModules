using AYellowpaper.SerializedCollections;
using UnityEngine;

public class DictionaryVariable<T1, T2> : ScriptableObject {
    [SerializedDictionary]
    public SerializedDictionary<T1, T2> Value = new SerializedDictionary<T1, T2>();

    private void OnEnable() {
        Value.Clear();
    }

    
}
