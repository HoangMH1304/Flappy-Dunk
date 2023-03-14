using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerHoop : MonoBehaviour
{
    private const string PLAYER = "Player";
    [SerializeField] private HoopController hoopController;
    [SerializeField] private ParticleSystem starFX, bigSmokeFX, blastFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                Debug.Log("Total");
                if (!hoopController.BorderInteract)
                {
                    GameController.Instance.IncreaseSwitch();
                    starFX.Play();
                    if(GameController.Instance.Swish > 1)
                    {
                        bigSmokeFX.Play();
                        blastFX.Play();
                    }
                }
                
                GameController.Instance.IncreseScore();
                // deactive old hoop and active new hoop
                hoopController.PassPoint();
                hoopController.PassOver = true;
                // mark that ball pass the hoop
            }
            else
            {
                Debug.LogError("Lose");
                GameController.Instance.DeactivePerfectForm();
                GameManager.Instance.ChangeState(GameState.OnDeath);
            }
        }
    }

    IEnumerator DisableHoop()
    {
        yield return new WaitForSeconds(0.3f);
        hoopController.gameObject.SetActive(false);
    }
}


