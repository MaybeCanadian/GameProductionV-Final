using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnHowToPlayButtonPressed()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    private void Start()
    {
        SoundManager.instance.ShuffleAllSongs(1.0f);
    }
}
