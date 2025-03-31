using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _audioManager;
    public Sound[] _soundEffects, _music, _player, _enemy, _environmental;
    public AudioSource _soundEffectSource, _musicSource, _playerSource, _enemySource, _environmentalSource;

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

    public void PlayPlayerSound(string name)
    {
        Sound effect = Array.Find(_player, track => track._name == name);
        if(effect == null) { return; }
        else
        {
            _playerSource.clip = effect._audio;
            _playerSource.Play();
        }
    }

    public void PlayEnemySound(string name)
    {
        Sound effect = Array.Find(_enemy, track => track._name == name);
        if(effect == null) { return; }
        else
        {
            _enemySource.clip = effect._audio;
            _enemySource.Play();
        }
    }

    public void PlayEnvironmentalSound(string name)
    {
        Sound effect = Array.Find(_environmental, track => track._name == name);
        if(effect == null) { return; }
        else
        {
            _environmentalSource.clip = effect._audio;
            _environmentalSource.Play();
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
