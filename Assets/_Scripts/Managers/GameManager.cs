using System;
using UnityEngine;
using UnityEngine.Events;
public enum GameState
{
    OnBegin,
    OnRevive,
    OnDeath,
    OnWin
}

public enum GameMode
{
    Endless,
    Challenge
}

public class GameManager : MonoSingleton<GameManager>
{
    public GameState State {get; private set;}
    public GameMode Mode { get; private set;}
    private bool isEndlessMode;
    private bool isChallengeMode;
    private bool secondChance;
    private bool playable;
    public bool SecondChance { get => secondChance; set => secondChance = value; }
    public bool Playable { get => playable; set => playable = value; }
    public bool IsEndlessMode { get => isEndlessMode; set => isEndlessMode = value; }
    public bool IsChallengeMode { get => isChallengeMode; set => isChallengeMode = value; }

    protected override void Awake() 
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }

    public void ChangeGameMode(GameMode newMode)
    {
        Mode = newMode;
        switch(newMode)
        {
            case GameMode.Challenge:
                isChallengeMode = true;
                isEndlessMode = false;
                return;
            case GameMode.Endless:
                isChallengeMode = false;
                isEndlessMode = true;
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(newMode), newMode, null);
        }
    }

    public void ChangePhase(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.OnBegin:
                HandleOnBeginState();
                // Logger.Log(newState.ToString());
                return;
            case GameState.OnRevive:
                HandleReviveState();
                // Logger.Log(newState.ToString());
                return;
            case GameState.OnDeath:
                HandleOnDeathState();
                // Logger.Log(newState.ToString());
                return;
            case GameState.OnWin:
                HandleOnWinState();
                // Logger.Log(newState.ToString());
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleOnBeginState()
    {
        secondChance = false;
        playable = true;
    }

    private void HandleOnDeathState()
    {
        if(playable)
        {
            playable = false;
            HoopManager.Instance.TurnOffCollider();
            UIIngameController.Instance.DisplayOnDeathUI();
        }
    }

    private void HandleReviveState()
    {
        playable = true;
        secondChance = true;
    }

    private void HandleOnWinState()
    {
        playable = false;
    }
}