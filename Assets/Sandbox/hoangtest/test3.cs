using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test3 : MonoBehaviour
{
    public Sprite newsprite;

    private void Awake()
    {
        Debug.Log("Awake");
        this.RegisterListener(EventID.OnActiveSecondChance, (param) => OnTest());
    }
    void Start()
    {
        Debug.Log("Start");
        //GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(newsprite);
        GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, newsprite);

    }

    private void OnTest()
    {
        gameObject.SetActive(false);
        Debug.Log("capsual");
    }
}
