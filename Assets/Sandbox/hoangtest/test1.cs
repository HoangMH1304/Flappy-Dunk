using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.RegisterListener(EventID.OnActiveSecondChance, (param) => OnTest());
    }

    private void OnTest()
    {
        gameObject.SetActive(false);
        Debug.Log("Square");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
