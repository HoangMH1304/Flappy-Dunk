using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem[] starFX;
    private bool reachGoal, jumpable;

    private void OnEnable() 
    {
        Debug.Log("Enable goal");
        reachGoal = false;
        jumpable = true;
    }

    public void InitialSpecs()
    {
        reachGoal = false;
        jumpable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.Playable)
        {
            GameManager.Instance.ChangePhase(GameState.OnWin);
            // LevelSpawner.Instance.currentLevel.SetFinishState(true);  //cong them 1 level da complete
            int levelID = PlayerPrefs.GetInt("LevelSelected");
            ChallengeController.Instance.IncreseLevelCompleted(levelID);
            UIIngameController.Instance.ShowWinResult();
            reachGoal = true;
            player.DeactivatePerfectForm();
            PlayEffect();
            StartCoroutine(DelaySwitchToMainMenu(3.8f));
        }
    }

    private void PlayEffect()
    {
        foreach (var effect in starFX)
        {
            effect.Play();
        }
        SoundManager.Instance.PlaySound(Sound.newBestScore);
    }

    private void Update()
    {
        if (!reachGoal || !jumpable) return;
        if (player.transform.position.y < -1f)
        {
            jumpable = false;
            player.Jump();
            StartCoroutine(DelayJump(0.4f));
        }
    }

    IEnumerator DelayJump(float time)
    {
        yield return new WaitForSeconds(time);
        jumpable = true;
    }

    IEnumerator DelaySwitchToMainMenu(float time)
    {
        yield return new WaitForSeconds(time);
        UIIngameController.Instance.SwitchToMainMenu();
    }
}
