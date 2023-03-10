using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictZone : MonoBehaviour
{
    private const string PLAYER = "Player";
    [SerializeField] private HoopController hoopChecker;

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag(PLAYER))
        {
            if(!hoopChecker.PassOver)
            {
                Debug.Log("Game over by extra collider");
                // Debug.Break();
                GameController.Instance.DeactivePerfectForm();
                GameManager.Instance.ChangeState(GameState.OnDeath);
            }
        }    
    }
}