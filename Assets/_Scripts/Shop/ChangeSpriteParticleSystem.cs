using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteParticleSystem : MonoBehaviour
{
    private Sprite newSprite;
    private void Awake()
    {
        //this.RegisterListener(EventID.OnChangeSkin, (param) => OnChangeSkin());
    }

    private void OnEnable()
    {

        OnChangeSkin();
    }

    private void OnChangeSkin()
    {
        newSprite = ShopController.Instance.hoops[PlayerPrefs.GetInt("HoopIdSelected")].starEffectSprite;
        GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, newSprite);   
    }
}
