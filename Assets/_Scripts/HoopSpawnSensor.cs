using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSpawnSensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag.Equals("Hoop"))
        {
            HoopSpawner.Instance.Spawn();
        }   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
         if(other.tag.Equals("Hoop"))
        {
            Debug.Log("active false");
            other.gameObject.SetActive(false);
        }  
    }
}
