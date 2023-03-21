using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SecondChance : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Image fill;
    [SerializeField] private Color orange;
    [SerializeField] private GameObject secondChanceBtn;
    private float _time;

    private void OnEnable()
    {
        InitialState();
        secondChanceBtn.transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            secondChanceBtn.transform.DOScale(new Vector3(1f, 1f, 1), 0.5f).SetEase(Ease.InOutSine);

        }).SetLoops(-1, LoopType.Yoyo);

        fill.DOColor(Color.yellow, 5.0f / 3).OnComplete(() =>
        {
            fill.DOColor(orange, 5.0f / 3).OnComplete(() =>
            {
                fill.DOColor(Color.red, 5.0f / 3);
            }
            );
        }
        );
    }

    private void OnDisable() {
        secondChanceBtn.transform.DORewind();
    }

    private void InitialState()
    {
        _time = time;
    }

    private void Update() 
    {
        if(_time <= 0.1)
        {
            UIIngameController.Instance.SwitchToMainMenu();
            return;
        }
        _time -= Time.deltaTime;  
        fill.fillAmount = _time / time;
    }
}
