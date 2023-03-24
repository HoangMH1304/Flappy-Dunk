using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoang : MonoBehaviour
{
    public void OnClick()
    {
        this.PostEvent(EventID.OnActiveSecondChance);
    }
}
