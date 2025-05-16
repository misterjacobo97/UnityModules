using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>
/// use a concrete listVariable type as T1, and the underlying type as T2
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public class ListReference<T1, T2> {

    public ListVariable<T2> list;

    public bool useGenericList = false;
    public List<T2> genericList;

    /// <summary>
    /// use if you want to initialise your own list variable via code
    /// </summary>
    /// <param name="newlist"></param>
    public virtual void AssignList(T1 newlist) {
        list = ScriptableObject.CreateInstance<ListVariable<T2>>();
    }

    public virtual List<T2> GetList() {
        if (useGenericList && genericList != null) {
            genericList = new();
        }

        return useGenericList ? genericList : list.Value;
    }
    public virtual void AddToList(T2 data) {
        GetList().Add(data);
    }

    public virtual bool Contains(T2 data) {
        if (GetList().Contains(data)) {
            return true;
        }
        
        return false;
    }
    public virtual T2 GetRandomFromList() {
        return GetList()[UnityEngine.Random.Range(0, GetList().Count - 1)];
    }


}

[Serializable]
public class TransformListReference : ListReference<TransformListVariable, Transform> {}


[Serializable]
public class Vector2ListReference : ListReference<Vector2ListVariable, Vector2> {}

[Serializable]
public class ScriptableObjectListReference : ListReference<ScriptableObjectListVariable, ScriptableObject> { }

[Serializable]
public class AudioClipListReference : ListReference<AudioClipListVariable, AudioClip> { }