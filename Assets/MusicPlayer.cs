public class MusicPlayer : BasicAudioListener
{
    protected override void UpdateVolume(float value)
    {
        _source.volume = value * 0.6f;
    }
}
