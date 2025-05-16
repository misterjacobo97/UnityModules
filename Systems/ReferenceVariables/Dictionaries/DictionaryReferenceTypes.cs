using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;


/// <summary>
/// use a concrete DictVariable type as T1, and the key type as T2, and the value type and T3.
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="T3"></typeparam>
/// 
public abstract class DictionaryReference<T1, T2, T3> {

    public DictionaryVariable<T2,T3> dict;

    public virtual SerializedDictionary<T2, T3> GetDict() {
        return (dict as DictionaryVariable<T2, T3>).Value;
    }
    public virtual void AddToDict(T2 key, T3 value) {
        (dict as DictionaryVariable<T2, T3>).Value.Add(key, value);
    }

    public virtual T3 TryGetValue(T2 key) {
        if ((dict as DictionaryVariable<T2, T3>).Value.TryGetValue(key, out T3 value)) {
            return value;
        }
        return default(T3);
    }

    public virtual bool KeyExists(T2 key) {
        if (dict.Value.ContainsKey(key)) {
            return true;
        }
        return false;
    }

    public virtual void Remove(T2 key) {
        dict.Value.Remove(key);
    }

}

[Serializable]
public class TransformDictionaryReference : DictionaryReference<TransformDictionaryVariable, Transform, Transform> { }