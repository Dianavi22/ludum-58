using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _quitPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        PlayGame();
    }

    public void CloseReset()
    {
        _quitPanel.SetActive(false);
    }
}