using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _audioManager;
    public Sound[] _soundEffects, _music;
    public AudioSource _soundEffectSource, _musicSource;

    void Awake()
    {
        if(_audioManager == null)
        {
            _audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic("BackgroundMusic");
    }

    public void PlayMusic(string name)
    {
        Sound song = Array.Find(_music, track => track._name == name);
        if(song == null) { return; }
        else
        {
            _musicSource.clip = song._audio;
            _musicSource.Play();
        }
    }

    public void PlaySoundEffect(string name)
    {
        Sound effect = Array.Find(_soundEffects, track => track._name == name);
        if(effect == null) { return; }
        else
        {
            _soundEffectSource.clip = effect._audio;
            _soundEffectSource.Play();
        }
    }

    public float SoundEffectLength(string name)
    {
        Sound effect = Array.Find(_soundEffects, track => track._name == name);
        if(effect == null) { return 0; }
        else
        {
            return effect._audio.length;
        }
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    public void ToggleSoundEffects()
    {
        _soundEffectSource.mute = !_soundEffectSource.mute;
    }
}
