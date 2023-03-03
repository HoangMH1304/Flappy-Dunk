using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSpawnSensor : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) 
    {
         if(other.tag.Equals("Hoop") && other.transform.position.x < this.transform.position.x)
        {
            Debug.Log("active false");
            HoopSpawner.Instance.Spawn();
            StartCoroutine(TurnOffState(other));
        }  
    }

    IEnumerator TurnOffState(Collider2D other)
    {
        yield return new WaitForSeconds(4f);
        other.gameObject.SetActive(false);
    }
}
