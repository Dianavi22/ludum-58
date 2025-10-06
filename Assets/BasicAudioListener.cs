using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BasicAudioListener : AudioPlayer
{
    protected AudioSource _source;

    protected virtual void Awake()
    {
        _source = GetComponent<AudioSource>();
        UpdateVolume(PlayerPrefs.GetFloat(PlayerPrefsData.VOLUME, 0.5f));
    }

	protected override void UpdateVolume(float value)
    {
        _source.volume = value;
    }
}
