using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem[] starFX;
    private bool reachGoal, jumpable;

    private void Start()
    {
        InitialSpecs();
    }

    public void InitialSpecs()
    {
        reachGoal = false;
        jumpable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.ChangePhase(GameState.OnWin);
        reachGoal = true;
        PlayEffect();
    }

    private void PlayEffect()
    {
        foreach (var effect in starFX)
        {
            effect.Play();
        }
        //SoundManager.Instance.PlaySound(sou)
    }

    private void Update()
    {
        if (!reachGoal || !jumpable) return;
        if(player.transform.position.y < -1f)
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
}
