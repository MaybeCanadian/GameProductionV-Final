using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIScript : MonoBehaviour
{
    public void OnPlayAgainPressed()
    {
        SceneManager.LoadScene("Level1");
    }
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
