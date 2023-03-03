using System;
using UnityEngine;

public enum GameState
{
    Standby,
    StartGame,
    EndGame
}
public class GameManager : MonoSingleton<GameManager>
{
    public GameState State {get; private set;}
    private bool gameStart;
    private bool gameOver;
    public bool GameStart { get => gameStart; set => gameStart = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }

    protected override void Awake() {
        Application.targetFrameRate = 60;
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Standby:
                HandleStandbyState();
                Logger.Log(newState.ToString());
                return;
            case GameState.StartGame:
                HandleStartGameState();
                Logger.Log(newState.ToString());
                return;
            case GameState.EndGame:
                HandleEndGameState();
                Logger.Log(newState.ToString());
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void HandleStandbyState()
    {
        gameStart = false;
        gameOver = false;
    }

    public void HandleStartGameState()
    {
        gameStart = true;
    }

    public void HandleEndGameState()
    {
        gameOver = true;
    }
}