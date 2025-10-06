using System;
using UnityEngine;

public class MusicPlayer : BasicAudioListener
{
    [SerializeField] private float _timeCode = 26;

    protected override void UpdateVolume(float value)
    {
        _source.volume = value * 0.6f;
    }

    private void Update()
    {
        if (!_source.isPlaying)
        {
            _source.time = _timeCode;
            _source.Play();
        }
    }
}
