using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerHoop : MonoBehaviour
{
    [SerializeField] private HoopChecker hoopChecker;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                Debug.Log("Total");
                // ScoreManager.Instance.IncreseScore();
                if (!hoopChecker.BorderInteract)
                {
                    GameController.Instance.IncreaseSwitch();
                    Logger.Log("Swish");
                    // SoundManager.Instance.PlaySound(SoundManager.Sound.x4);
                }
                
                GameController.Instance.IncreseScore();
                // Logger.Log("NonSwish");

                // deactive old hoop and active new hoop
                // StartCoroutine(DisableHoop());
                // HoopManager.Instance.GetReadyHoop().SetActive(true);
                hoopChecker.PassPoint();
                hoopChecker.PassOver = true;
                // mark that ball pass the hoop
                //spawn new hoop _ hoopChecker.Spawn()    
            }
            else
            {
                Debug.LogError("Lose");
                GameManager.Instance.ChangeState(GameState.SecondChance);
            }
        }
    }

    IEnumerator DisableHoop()
    {
        yield return new WaitForSeconds(0.3f);
        hoopChecker.gameObject.SetActive(false);
    }
}


