using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _credits;
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
}