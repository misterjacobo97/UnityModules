using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : PersistentSignleton<LevelManager> {

    [NonSerialized] public UnityEvent LevelLoadingStarted = new(); // for the transition Overlay 
    [NonSerialized] public UnityEvent<string> LevelLoaded = new(); // string - level name
    [NonSerialized] public UnityEvent NewCheckpointSet = new(); // int - checkpointID

    [NonSerialized] public UnityEvent RestartedFromCheckpoint = new();

    [SerializeField] private CanvasGroup loadingScreen;

    [SerializeField] private string startingLevelTitle;
    public Transform CurrentSpawnpoint { get; private set; }
    public string LevelTitle { get; private set; }

    /// <summary>
    /// takes a string name and loads the level of that name - then sets the LevelTitle and invokes an event so that other scripts can update themselves
    /// </summary>
    /// <param name="levelName"></param>
    /// <returns></returns>
    public async Awaitable LoadLevel(string levelName) {

        // play cross fade animations and pre loading actions
        await DOTween.To(() => loadingScreen.alpha, x => loadingScreen.alpha = x, 1, 1).AsyncWaitForCompletion();
        
        LevelLoadingStarted.Invoke();

        await Awaitable.WaitForSecondsAsync(1);

        // wait till animation ends
        await SceneManager.LoadSceneAsync(levelName);

        // load new scene
        LevelTitle = levelName;
        LevelLoaded.Invoke(LevelTitle);

        await DOTween.To(() => loadingScreen.alpha, x => loadingScreen.alpha = x, 0, 1).AsyncWaitForCompletion();
    }

    public void SetSpawnpoint(Transform newSpawnpoint) {
        /*
            to be used by the spawnpoint as it spawns in
        */
        CurrentSpawnpoint = newSpawnpoint;
        Debug.Log("new spawnpoint");
        NewCheckpointSet.Invoke();
    }

    public async void RestartLevel() {
        await LoadLevel(LevelTitle);
    }

}

