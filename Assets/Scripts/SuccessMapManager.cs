using System.Collections;
using System.Collections.Generic;
using Rewards.Utils;
using UnityEngine;

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

    /// <summary>
    /// Achievement fader to fade in/out the panel on achievement obtained.
    /// </summary>
    [SerializeField] private UIFader _achievementFader;

    private void Start()
    {
        //GetAllSuccessState();
        ShowStateSuccess();
        _achievementFader.FadeOut(0);
    }

    private void OnDestroy()
    {
        PlayerPrefsUtils.SetBool(PlayerPrefsData.HAS_QUIT_THE_GAME, true);

        GetAllSuccessState();
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

    private void Update()
    {
        //Test :
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerPrefsUtils.SetBool(PlayerPrefsData.HAS_RESPECTED_CREATORS, true);
            GetAllSuccessState();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _achievementFader.FadeIn(2, then: () => StartCoroutine(WaitThenFadeOut()));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowSuccessMap();
        }
    }

    private IEnumerator WaitThenFadeOut()
    {
        yield return _displayDuration;
        _achievementFader.FadeOut(2);
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
       
        /*if (id == _success.Count)
        {
            _theKey.SetActive(true);
            _theList.SetActive(false);
        }*/

    }

    public void ShowSuccessMap()
    {
        _map.SetActive(!_map.activeSelf);
    }
}
