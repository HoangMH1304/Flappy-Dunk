using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    [SerializeField] private int value = 10;
    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();  
        spr.DOFade(1, 5).SetUpdate(true);  
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            value *= 10;
            spr.DOFade(1, 5).SetUpdate(true);
        }
    }
}
