using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelSelector;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _controls;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenCredits()
    {
        _credits.SetActive(true);
    }
    public void CloseCredits()
    {
        _credits.SetActive(false);
    }

    public void OpenControls()
    {
        _controls.SetActive(true);
    }
    public void CloseControls()
    {
        _controls.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}