using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayUIScript : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
