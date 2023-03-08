using System;
using UnityEngine;
using UnityEngine.Events;
public enum GameState
{
    Standby,
    StartGame,
    SecondChance,
    EndGame
}
public class GameManager : MonoSingleton<GameManager>
{
    public GameState State {get; private set;}
    private bool gameStart;
    private bool secondChance;
    private bool gameOver;
    private bool playable = true;
    public bool GameStart { get => gameStart; set => gameStart = value; }
    public bool SecondChance { get => secondChance; set => secondChance = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    public bool Playable { get => playable; set => playable = value; }

    protected override void Awake() {
        base.Awake();
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
            case GameState.SecondChance:
                HandleSecondChanceState();
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


    private void HandleStandbyState()
    {
        gameStart = false;
        secondChance = false;
        gameOver = false;
        playable = true;
    }

    private void HandleStartGameState()
    {
        gameStart = true;
    }
    private void HandleSecondChanceState()
    {
        if(!secondChance)
        {
            UIController.Instance.DisplayGameOverUI();
            secondChance = true;
        }
        playable = false;
    }

    private void HandleEndGameState()
    {
        gameOver = true;
    }
}