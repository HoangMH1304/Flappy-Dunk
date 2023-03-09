using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChance : MonoBehaviour
{
    private bool isActive;

    public bool IsActive { get => isActive; set => isActive = value; }

    private void OnEnable() 
    {
        isActive = false;    
    }
}
