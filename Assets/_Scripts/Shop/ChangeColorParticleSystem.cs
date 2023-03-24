using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorParticleSystem : MonoBehaviour
{
    private Color newColor;

    private void Start()
    {
        this.RegisterListener(EventID.OnChangeSkin, (param) => OnChangeSkin());
    }

    private void OnChangeSkin()
    {
        Debug.LogError($"Flame ID: {PlayerPrefs.GetInt("FlameIdSelected")}");
        newColor = ShopController.Instance.flames[PlayerPrefs.GetInt("FlameIdSelected")].color;
        ParticleSystem.MainModule ps = GetComponent<ParticleSystem>().main;
        ps.startColor = newColor;
    }

    private void OnEnable()
    {
        OnChangeSkin();
    }
}
