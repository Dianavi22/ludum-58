using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[Serializable]
public class SfxSource : SerializableDictionaryBase<string, AudioClip> { }

public class PlayerSFXManager : BasicAudioListener
{
	[SerializeField] SfxSource _sfxs;
	[SerializeField] GameObject _sfxPlayer;

	public bool IsPlayingWalk => _source.isPlaying;

	private float _volume;

	private AudioSource _danceSrc;

	protected override void UpdateVolume(float value)
	{
		base.UpdateVolume(value);
		_volume = value;
	}

	public void PlayInteract()
	{
		PlaySfx("Interact");
	}

	public void PlayWalk()
	{
		//_source.Play();
	}

	public void StopWalk()
	{
		//_source.Stop();
	}

	public void PlayHitGround()
	{
		PlaySfx("HitGround");
	}

	public void PlayDeath()
	{
		PlaySfx("Death");
	}

	public void PlayJump()
	{
		PlaySfx("Jump" + UnityEngine.Random.Range(0, 2));
	}

	private void PlaySfx(string key)
	{
		AudioSource source = Instantiate(_sfxPlayer).GetComponent<AudioSource>();
		source.volume = _volume;
		source.clip = _sfxs[key];
		source.Play();

		if(key == "Dance")
		{
			_danceSrc = source;
		}

		Destroy(source.gameObject, key == "Dance" ? 30f : 2f);
	}

    internal void PlayDance()
    {
		PlaySfx("Dance");
    }

    internal void StopDance()
    {
		Destroy(_danceSrc);
    }
}
