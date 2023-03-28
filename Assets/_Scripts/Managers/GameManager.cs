using System;
using UnityEngine;
using UnityEngine.Events;
public enum GameState
{
    OnBegin,
    OnRevive,
    OnDeath,
    OnWin,
    OnTrial,
    OnExitTrial
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
    private bool trial;
    public bool SecondChance { get => secondChance; set => secondChance = value; }
    public bool Playable { get => playable; set => playable = value; }
    public bool IsEndlessMode { get => isEndlessMode; set => isEndlessMode = value; }
    public bool IsChallengeMode { get => isChallengeMode; set => isChallengeMode = value; }
    public bool Trial { get => trial; set => trial = value; }

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
                HandleChallengeMode();
                return;
            case GameMode.Endless:
                HandleEndlessMode();
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(newMode), newMode, null);
        }
    }

    private void HandleChallengeMode()
    {
        isChallengeMode = true;
        isEndlessMode = false;
    }

    private void HandleEndlessMode()
    {
        isChallengeMode = false;
        isEndlessMode = true;
    }

    public void ChangePhase(GameState newState)
    {
        State = newState;
        // Logger.Log(newState.ToString());
        switch (newState)
        {
            case GameState.OnBegin:
                HandleOnBeginState();
                return;
            case GameState.OnRevive:
                HandleReviveState();
                return;
            case GameState.OnDeath:
                HandleOnDeathState();
                return;
            case GameState.OnWin:
                HandleOnWinState();
                return;
            case GameState.OnTrial:
                HandleOnTrialState();
                return;
            case GameState.OnExitTrial:
                HandleOnExitTrialState();
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleOnExitTrialState()
    {
        //swap last skin
        trial = false;
        string lastItem = PlayerPrefs.GetString("ItemIdLastSelected");
        string[] item = lastItem.Split();
        PlayerPrefs.SetInt(item[0] + "IdSelected", Int32.Parse(item[1]));
        this.PostEvent(EventID.OnChangeSkin);
    }

    private void HandleOnTrialState()
    {
        trial = true;
        this.PostEvent(EventID.OnChangeSkin);
        UILobbyController.Instance.Play();
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
            // trial = false;
            HoopManager.Instance.TurnOffCollider();
            UIIngameController.Instance.DisplayOnDeathUI();
        }
    }

    private void HandleReviveState()
    {
        playable = true;
        secondChance = true;
        this.PostEvent(EventID.OnActiveSecondChance);
    }

    private void HandleOnWinState()
    {
        playable = false;
    }

    private void OnDisable() {
        if(trial) HandleOnExitTrialState();
    }
}