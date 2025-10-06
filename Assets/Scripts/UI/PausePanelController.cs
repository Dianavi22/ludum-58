using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] SuccessMapManager successMapManager;
    public float SliderValue { get; private set; }
    /// <summary>
    /// The volume slider associated with this pause panel.
    /// </summary>
    [SerializeField] private Slider _slider;

    /// <summary>
    /// The mute button icon swapper of this pause panel.
    /// </summary>
    [SerializeField] private IconSwapper _muteButtonSwapper;

    /// <summary>
    /// The text mesh pro associated with the volume slider.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _volumeBarTMP;

    /// <summary>
    /// The current volume. Will not be updated to 0 if muted through the mute button.
    /// </summary>
    private float _volume;

    /// <summary>
    /// Gets the percentage of the volume.
    /// </summary>
    public int Volume => Mathf.RoundToInt(_volume * 100);
    public float RealVolume => _volume;

    private UnityEvent<float> _onVolumeChanged = new();
    public UnityEvent<float> OnVolumeChanged => _onVolumeChanged;

    /// <summary>
    /// Save of the previous volume when muting through the mute button.
    /// </summary>
    private float _previousVolume;

    /// <summary>
    /// Whether the volume is fully muted or not (either muted through the button or the [_slider]'s value is 0).
    /// </summary>
    private bool _isMuted;

    [SerializeField] PlayerSFXManager _soundManager;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat(PlayerPrefsData.VOLUME, 0.5f);
        _slider.value = volume;
        ChangeVolume(volume);
    }

    /// <summary>
    /// Callback when the fullscreen button is pressed.
    /// </summary>
    public void OnFullscreenPressed()
    {
        Screen.fullScreen = !Screen.fullScreen;
        successMapManager.LaunchSuccessAnim(PlayerPrefsData.WINDOW_SUCCESS);
        //TODO: persistant data
    }

    /// <summary>
    /// Callback when the main menu button is pressed.
    /// </summary>
    public void OnMainMenuPressed()
    {
        Debug.Log("TODO: return to main menu");
    }

    /// <summary>
    /// Callback when the resume button is pressed.
    /// </summary>
    public void OnResumePressed()
    {
        Debug.Log("TODO: dismiss the pause");
    }

    /// <summary>
    /// Callback when the [_slider] value changes.
    /// </summary>
    public void OnSliderChanged()
    {
        ChangeVolume( _slider.value);


    }

    /// <summary>
    /// Callback when the mute button is pressed.
    /// </summary>
    public void OnMutePressed()
    {
        if (_isMuted)
        {
            _slider.value = _previousVolume;
        }
        else
        {
            _previousVolume = _volume;
            _slider.value = 0;
        }
    }

    /// <summary>
    /// Inner callback for changing value. Updates [_isMuted] accordingly.
    /// </summary>
    /// <param name="value">The value to set the volume to</param>
    private void ChangeVolume(float value)
    {
        PlayerPrefs.SetFloat(PlayerPrefsData.VOLUME, value);
        _onVolumeChanged?.Invoke(value);
        _volume = value;
        _isMuted = value == 0;
        _volumeBarTMP.text = Mathf.RoundToInt(_volume * 100) + "%";
        SliderValue = value;

        _muteButtonSwapper.SwapTo(!_isMuted);
    }
}
