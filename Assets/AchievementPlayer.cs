public class AchievementPlayer : BasicAudioListener
{
    protected override void UpdateVolume(float value)
    {
        _source.volume = value * 0.3f;
    }
}
