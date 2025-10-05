using System.Collections;
using System.Collections.Generic;
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

    public static bool isFading { get; private set; } = false;
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
        GetAllSuccessState();
        ShowStateSuccess();

        // 9 is the index of quit achievement in [_success]
        if (!PlayerPrefsUtils.TryGetBool(PlayerPrefsData.IS_THE_FIRST_TIME, true) && !_success[9].SuccessDatas.isSuccess)
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
        _successPanel.SetActive(true);

        for (int i = 0; i < _success.Count; i++)
        {
            if (_success[i].SuccessDatas.successKey == key)
            {
                _iconSuccessAnim.sprite = _success[i].SuccessDatas.successSprite;
                _titleSuccessAnim.text = _success[i].SuccessDatas.successName;
                _descriptionSuccessAnim.text = _success[i].SuccessDatas.successDescription;
            }
        }
        if (PlayerPrefsUtils.TryGetBool(key))
        {
            return;
        }
        PlayerPrefsUtils.SetBool(key, true);
        GetAllSuccessState();
        ShowStateSuccess();
        try { }
        catch
        {
            return;
        }
        isFading = true;
        _achievementFader.FadeIn(_fadeDuration);
    }

    public void GetAllSuccessState()
    {
        for (int i = 0; i < _success.Count; i++)
        {
            _success[i].SuccessDatas.isSuccess = PlayerPrefsUtils.TryGetBool(_success[i].SuccessDatas.successKey);
            if (_success[i].SuccessDatas.isSuccess)
            {
                _button.SetActive(true);
            }
        }

    }

    private void ClearSuccessPanel()
    {
        _titleSuccessAnim.text = "";
        _descriptionSuccessAnim.text = "";
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && _successPanel.activeSelf)
        {
            isFading = false;
            _achievementFader.FadeOut(_fadout, () => _successPanel.SetActive(false));
            ClearSuccessPanel();
            

        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowSuccessMap();
        }
    }

    
    private void ShowStateSuccess()
    {
        CheckAllSuccess();
        for (int i = 0; i < _success.Count; i++)
        {
            if (_success[i].SuccessDatas.isSuccess)
            {
                _success[i].ShowAllSuccess();
            }

            else if (_success[i].SuccessDatas.showImage)
            {
                _success[i].ShowOnlySprite();
            }

            else if (_success[i].SuccessDatas.showText)
            {
                _success[i].ShowOnlyName();
            }
        }
    }

    private void CheckAllSuccess()
    {
        int id = 0;
        for (int i = 0; i < _success.Count; i++)
        {

            if (_success[i].SuccessDatas.isSuccess)
            {
                id++;
            }
        }

        if (id == _success.Count)
        {
            print("ALL SUCCESS");
        }
    }

    public void ShowSuccessMap()
    {
        if (SuccessMapManager.isFading || PauseMenu.IsPause || PauseMenu.IsMainMenu) { return; }
        LaunchSuccessAnim(PlayerPrefsData.PAUSE_MENU);
        _map.SetActive(!_map.activeSelf);
    }
}
