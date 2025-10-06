using UnityEngine;

public abstract class AudioPlayer : MonoBehaviour
{
    [SerializeField] protected PausePanelController _pauseController;

    protected virtual void Start()
    {
        _pauseController.OnVolumeChanged.AddListener(UpdateVolume);
    }

    protected virtual void OnDestroy()
    {
        _pauseController.OnVolumeChanged.RemoveListener(UpdateVolume);
    }

    protected abstract void UpdateVolume(float value);
}
