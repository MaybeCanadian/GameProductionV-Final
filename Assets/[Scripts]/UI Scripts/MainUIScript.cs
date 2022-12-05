using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainUIScript : MonoBehaviour
{
    public void QuitGame(InputAction.CallbackContext context)
    {
        if (context.started)
            Application.Quit();
    }
}
