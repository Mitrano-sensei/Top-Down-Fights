using KBCore.Refs;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using Utilities;


/**
 * A really simple audio manager that plays one shot sounds.
 */
[RequireComponent(typeof(AudioSource))]
public class SimpleAudioManager : Singleton<SimpleAudioManager>
{
    [SerializeField] private AudioSource _soundsSource;

    [Button]
    public void PlaySound(AudioClip clip)
    {
        _soundsSource.PlayOneShot(clip);
    }
}
