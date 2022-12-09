using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GameStateMachine.GetInstance().ChangeState(GameStates.GAME);
    }

    private void OnEnable()
    {
        GameStateMachine.OnStateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        GameStateMachine.OnStateChanged -= OnStateChanged;
    }

    public void OnStateChanged(GameStates newState, GameStates oldState)
    {
        
        if(newState == GameStates.GAME && oldState == GameStates.MAINMENU)
        {
            
            SoundManager.instance.ShuffleAllSongs(1.0f);
            return;
        }

        
    }
}
