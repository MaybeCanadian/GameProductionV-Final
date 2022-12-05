using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    static GameStateMachine instance;

    public delegate void StateChanged(GameStates newState, GameStates oldState);
    public static StateChanged OnStateChanged;

    private GameStates currentState;
    
    public static GameStateMachine GetInstance()
    {
        if(instance == null)
        {
            instance = new GameStateMachine();

            instance.currentState = GameStates.GAME;
        }

        return instance;
    }

    public GameStates GetState()
    {
        return currentState;
    }

    public void ChangeState(GameStates input)
    {
        OnStateChanged?.Invoke(input, currentState);
        currentState = input;
    }
}

[System.Serializable]
public enum GameStates
{
    GAME,
    INVENTORY,
    RECIPE,
    BUILDING,
    PAUSE,
    UPGRADE
}
