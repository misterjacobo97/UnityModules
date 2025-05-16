using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : PersistentSignleton<SoundManager> {
    [SerializeField] private AudioSource musicSource;
    
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _effectMixer;


    [SerializeField] private int maxEffectSources = 10;
    private List<AudioSource> _fxSourceList = new();

    protected override void Awake(){
        base.Awake();

        for(int i = 0; i < maxEffectSources; i++){
            AudioSource newSource = new GameObject($"FX_audioSouce_{_fxSourceList.Count}", typeof(AudioSource)).GetComponent<AudioSource>();

            newSource.transform.SetParent(this.transform);
            
            _fxSourceList.Add(newSource);
        }
    }

    public void PlayMusicTrack(AudioClip clip) {
        musicSource.Stop();

        musicSource.outputAudioMixerGroup = _musicMixer;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySound(AudioClip clip, float volume = 1, float startClipAt = 0, AudioMixerGroup mixerGroup = null) {
        // check if max amount of sources has been reached
        if (_fxSourceList.Any(s => s.isPlaying == false)){
            // create new source
            AudioSource newSource = _fxSourceList.First(s => s.isPlaying == false);

            newSource.clip = clip;
            newSource.time = startClipAt;
            newSource.outputAudioMixerGroup = mixerGroup ? mixerGroup : _effectMixer;
            newSource.loop = false;
            newSource.volume = volume;
            newSource.Play();

            // destroy after length of time
            StopAudioSource(newSource, newSource.clip.length);
        }
    }

    private async void StopAudioSource(AudioSource source, float delay) {
        await Awaitable.WaitForSecondsAsync(delay);

        source.Stop();
    }
}
