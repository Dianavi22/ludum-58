using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewards.Utils;
using TMPro;
using UnityEngine;
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
    [SerializeField] GameObject _successPanel;
    [SerializeField] PauseMenu _pauseMenu;

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

    private void Start()
    {
        _achievementFader.FadeOut(0);
        GetAllSuccessState();
        ShowStateSuccess();

        if (PlayerPrefsUtils.TryGetBool(PlayerPrefsData.IS_THE_FIRST_TIME, true) && !_success.First(success => success.SuccessDatas.successKey == PlayerPrefsData.HAS_QUIT_THE_GAME).SuccessDatas.isSuccess)
        {
            LaunchSuccessAnim(PlayerPrefsData.HAS_QUIT_THE_GAME);
        }
    }

    private void OnDestroy()
    {
        PlayerPrefsUtils.SetBool(PlayerPrefsData.HAS_QUIT_THE_GAME, true);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefsUtils.SetBool(PlayerPrefsData.IS_THE_FIRST_TIME, false);
    }

    public void LaunchSuccessAnim(string key)
    {
        Success success = _success.First(sucess => sucess.SuccessDatas.successKey == key);

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
    }

    public void GetAllSuccessState()
    {
        _button.SetActive(_success.Any(success => PlayerPrefsUtils.TryGetBool(success.SuccessDatas.successKey)));
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
            });
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowSuccessMap();
        }
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
        }
    }

    private void CheckAllSuccess()
    {
        bool all = _success.All(success => success.SuccessDatas.isSuccess);

        if (all)
        {
            print("ALL SUCCESS");
        }
    }

    public void ShowSuccessMap()
    {
        if (IsFading || PauseMenu.IsPause || PauseMenu.IsMainMenu) { return; }
        LaunchSuccessAnim(PlayerPrefsData.PAUSE_MENU);
        _map.SetActive(!_map.activeSelf);
    }
}
