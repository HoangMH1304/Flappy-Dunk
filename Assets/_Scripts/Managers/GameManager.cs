using System;
using UnityEngine;
using UnityEngine.Events;
public enum GameState
{
    OnBegin,
    OnRevive,
    OnDeath
}
public class GameManager : MonoSingleton<GameManager>
{
    public GameState State {get; private set;}
    private bool secondChance;
    private bool playable = true;
    public bool SecondChance { get => secondChance; set => secondChance = value; }
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
            case GameState.OnBegin:
                HandleOnBeginState();
                Logger.Log(newState.ToString());
                return;
            case GameState.OnRevive:
                HandleReviveState();
                Logger.Log(newState.ToString());
                return;
            case GameState.OnDeath:
                HandleOnDeathState();
                Logger.Log(newState.ToString());
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
            UIController.Instance.DisplayOnDeathUI();
        }
    }

    //when click revive button
    private void HandleReviveState()
    {
        playable = true;
        secondChance = true;
    }
}