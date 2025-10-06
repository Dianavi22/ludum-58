using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewards.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SuccessMapManager : MonoBehaviour
{
    /// <summary>
    /// The achievement display duration.
    /// </summary>
    private static readonly WaitForSeconds _displayDuration = new(5);

    [SerializeField] List<Success> _success = new List<Success>();
    [SerializeField] GameObject _theKey;
    [SerializeField] GameObject _theList;
    [SerializeField] GameObject _map;
    [SerializeField] GameObject _button;
    [SerializeField] GameObject _mapHidden;
    [SerializeField] GameObject _doorToRemove;
    [SerializeField] GameObject _wallToRemove;
    [SerializeField] GameObject _cameraBoundToRemove;
    [SerializeField] GameObject _successPanel;
    [SerializeField] GameObject _theEndPanel;
    [SerializeField] private PauseMenu _pauseMenu;

    public static bool IsFading { get; private set; } = false;
    [SerializeField] float _fadeDuration;
    [SerializeField] float _fadout;

    [SerializeField] TMP_Text _titleSuccessAnim;
    [SerializeField] TMP_Text _descriptionSuccessAnim;
    [SerializeField] Image _iconSuccessAnim;

    [SerializeField] TypeSentence _typeSentence;
    /// <summary>
    /// Achievement fader to fade in/out the panel on achievement obtained.
    /// </summary>
    [SerializeField] private UIFader _achievementFader;

    [SerializeField] PostProcessVolume m_Volume;
    [SerializeField] Vignette m_Vignette;
    [SerializeField] List<Animator> _successAnimators;
    [SerializeField] ParticleSystem _glowSuccessPart;

    private void Start()
    {
        GetAllSuccessState();
        ShowStateSuccess();
        m_Volume.profile.TryGetSettings(out m_Vignette);
        if (PlayerPrefsUtils.GetBool(PlayerPrefsData.IS_NOT_THE_FIRST_TIME) && !_success.Where(success => success.SuccessDatas.successKey == PlayerPrefsData.HAS_QUIT_THE_GAME).Any(success => success.SuccessDatas.isSuccess))
        {
            LaunchSuccessAnim(PlayerPrefsData.HAS_QUIT_THE_GAME);
        }
    }
    private Coroutine vignetteCoroutine;


    public void FadeInVignette(float duration = 0.3f, float targetIntensity = 0.45f)
    {
        if (m_Vignette != null)
        {
            StartVignetteLerp(targetIntensity, duration);
        }
    }

    public void FadeOutVignette(float duration = 0.3f)
    {
        if (m_Vignette != null)
        {
            StartVignetteLerp(0f, duration);
        }
    }

    private void StartVignetteLerp(float target, float duration)
    {
        if (vignetteCoroutine != null)
            StopCoroutine(vignetteCoroutine);

        vignetteCoroutine = StartCoroutine(LerpVignette(target, duration));
    }

    private IEnumerator LerpVignette(float target, float duration)
    {
        float start = m_Vignette.intensity.value;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            m_Vignette.intensity.value = Mathf.Lerp(start, target, time / duration);
            yield return null;
        }

        m_Vignette.intensity.value = target;
    }
#if UNITY_EDITOR
    private void OnDestroy()
    {
        if (!CheckFinal())
        {
            PlayerPrefsUtils.SetBool(PlayerPrefsData.IS_NOT_THE_FIRST_TIME, true);
        }
    }
#endif

    private void OnApplicationQuit()
    {
        if (!CheckFinal())
        {
            PlayerPrefsUtils.SetBool(PlayerPrefsData.IS_NOT_THE_FIRST_TIME, true);
        }
    }

    public void LaunchSuccessAnim(string key)
    {
        Success success = _success.FirstOrDefault(sucess => sucess.SuccessDatas.successKey == key);

        if (success == null)
        {
            return;
        }

        _iconSuccessAnim.sprite = success.SuccessDatas.successSprite;
        _titleSuccessAnim.text = success.SuccessDatas.successName;
        _descriptionSuccessAnim.text = success.SuccessDatas.successDescription;

        if (PlayerPrefsUtils.TryGetBool(key))
        {
            return;
        }

        PlayerPrefsUtils.SetBool(key, true);
        GetAllSuccessState();
        ShowStateSuccess();

        IsFading = true;
        _achievementFader.FadeIn(_fadeDuration);
        FadeInVignette();
        _successAnimators.ForEach(animator => animator.SetBool("IsVisible", true));
        Invoke("GlowPart", 0.60f);

    }

    private void GlowPart()
    {
        _glowSuccessPart.gameObject.SetActive(true);
        _glowSuccessPart.Play();

    }

    public void GetAllSuccessState()
    {
        bool any = false;
        foreach (Success success in _success)
        {
            bool isSuccess = PlayerPrefsUtils.TryGetBool(success.SuccessDatas.successKey);
            success.SuccessDatas.isSuccess = isSuccess;
            any |= isSuccess;
        }

        _button.SetActive(any);
        _mapHidden.SetActive(any);
    }

    private void ClearSuccessPanel()
    {
        _titleSuccessAnim.text = "";
        _descriptionSuccessAnim.text = "";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _achievementFader.isFadeIn)
        {
            _achievementFader.FadeOut(_fadout, () =>
            {
                IsFading = false;
                ClearSuccessPanel();
                _successAnimators.ForEach(animator => animator.SetBool("IsVisible", false));
                FadeOutVignette();
                _glowSuccessPart.gameObject.SetActive(false);
                if (CheckAllSuccess())
                {
                    Invoke("TriggerEnding", 0.5f);
                }

                if (CheckFinal())
                {
                    _wallToRemove.SetActive(false);
                    _doorToRemove.SetActive(false);
                    _cameraBoundToRemove.SetActive(false);
                    //Invoke("ShowTheEnd", 0.5f);
                }
            });
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowSuccessMap();
        }

        if (Input.anyKeyDown && _theEndPanel.activeInHierarchy && _theEndPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefsUtils.SetBool(PlayerPrefsData.IS_NOT_THE_FIRST_TIME, false);
            Application.Quit();
        }
    }

    private void TriggerEnding()
    {
        LaunchSuccessAnim(PlayerPrefsData.FINAL_SUCCESS);
    }

    public void ShowTheEnd()
    {
        PauseMenu.IsInTheEnd = true;
        _theEndPanel.SetActive(true);
    }

    private void ShowStateSuccess()
    {
        CheckAllSuccess();
        foreach (Success success in _success)
        {
            if (success.SuccessDatas.isSuccess)
            {
                success.ShowAllSuccess();
            }

            else if (success.SuccessDatas.showImage)
            {
                success.ShowOnlySprite();
            }

            else if (success.SuccessDatas.showText)
            {
                success.ShowOnlyName();
            }
            else
            {
                success.ShowNothing();
            }
        }
    }

    private bool CheckAllSuccess()
    {
        return _success.Except(_success.Where(success => success.SuccessDatas.successKey == PlayerPrefsData.FINAL_SUCCESS)).All(success => success.SuccessDatas.isSuccess);
    }

    private bool CheckFinal()
    {
        return _success.All(success => success.SuccessDatas.isSuccess);
    }

    public void ShowSuccessMap()
    {
        if (IsFading || PauseMenu.IsPause || PauseMenu.IsMainMenu) { return; }
        LaunchSuccessAnim(PlayerPrefsData.OPEN_SUCCESS);
        _map.SetActive(!_map.activeSelf);
    }
}
