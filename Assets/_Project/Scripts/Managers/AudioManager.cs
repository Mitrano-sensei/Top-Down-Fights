using KBCore.Refs;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _soundsSource;

    [Button]
    public void PlaySound(AudioClip clip)
    {
        _soundsSource.PlayOneShot(clip);
    }
}
