using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    void Start()
    {
        this.RegisterListener(EventID.OnActiveSecondChance, (param) => OnTest());
    }

    private void OnTest()
    {
        gameObject.SetActive(false);
        Debug.Log("circle");
    }
}
