using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private ParticleSystem[] particleSystems;
    private Sprite newSprite;


    private void Awake()
    {
        this.RegisterListener(EventID.OnCongratulation, (param) => OnParticleSystemActive());
    }

    public void OnParticleSystemActive()
    {
        newSprite = ShopController.Instance.hoops[PlayerPrefs.GetInt("HoopIdSelected")].starEffectSprite;
        foreach (var item in particleSystems)
        {
            item.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, newSprite);
            item.Play();
        }
        SoundManager.Instance.PlaySound(Sound.newBestScore);
    }
}
