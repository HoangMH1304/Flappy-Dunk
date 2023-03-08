using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionChecker : MonoBehaviour
{
    [SerializeField] private HoopChecker hoopChecker;

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(!hoopChecker.PassOver)
            {
                Debug.Log("Game over by extra collider");
                // Debug.Break();
                GameManager.Instance.ChangeState(GameState.SecondChance);
            }
        }    
    }
}