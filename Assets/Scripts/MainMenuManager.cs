using System.Collections;
using System.Collections.Generic;
using Rewards.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _quitPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    public void PlayGame()
    {
        _quitPanel.SetActive(false);
        _mainMenuPanel.SetActive(false);
        PauseMenu.IsMainMenu = false;
    }
    
    public void OpenCredits()
    {
        _credits.SetActive(true);
    }
    public void CloseCredits()
    {
        _credits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetParty()
    {
        _quitPanel.SetActive(true);
    }

    public void HardReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefsUtils.SetBool(PlayerPrefsData.IS_NOT_THE_FIRST_TIME, false);
        PlayGame();
    }

    public void CloseReset()
    {
        _quitPanel.SetActive(false);
    }
}