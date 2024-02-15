using KBCore.Refs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _soundsSource;

    public void PlaySound(AudioClip clip)
    {
        _soundsSource.PlayOneShot(clip);
    }
}
