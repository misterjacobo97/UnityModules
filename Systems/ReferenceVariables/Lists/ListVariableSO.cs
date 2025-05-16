using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListVariable<T> : ScriptableObject {
    // [TypeMismatchFix]
    [SerializeReference] public List<T> Value = new List<T>();

    [SerializeField] private bool clearOnSceneChange = false;

    private void OnEnable() {
        if (clearOnSceneChange) {
            SceneManager.activeSceneChanged += (a, b) => { Value.Clear(); };
        }
    }
}

