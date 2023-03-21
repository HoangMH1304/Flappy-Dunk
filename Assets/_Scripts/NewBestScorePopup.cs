using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBestScorePopup : MonoBehaviour
{
    [SerializeField] private RectTransform newBestScoreText;
    [SerializeField] private RectTransform newBestScoreFlare;

    private void OnEnable() 
    {
        newBestScoreFlare.DOScale(1, 0.5f);
        Tilt();
        SoundManager.Instance.PlaySound(Sound.newBestScore);
    }

    private void Tilt()
    {
        newBestScoreText.DORotate(Vector3.forward * 6f, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            newBestScoreText.DORotate(Vector3.forward * -6f, 1f).SetEase(Ease.InOutSine);
        }).SetLoops(3, LoopType.Yoyo).OnComplete(() =>
        {
            newBestScoreText.DORotate(Vector3.forward * -6f, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                newBestScoreFlare.DOScale(0, 0.3f);
                newBestScoreText.DOScale(0, 0.3f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });
        });
    }

    private void OnDisable() 
    {
        newBestScoreFlare.DORewind();
        newBestScoreText.DORewind();
    }
}
