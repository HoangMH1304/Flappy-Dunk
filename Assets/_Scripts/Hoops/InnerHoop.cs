using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerHoop : MonoBehaviour
{
    private const string PLAYER = "Player";
    [SerializeField] private HoopController hoopController;
    [SerializeField] private ParticleSystem starFX, bigSmokeFX, blastFX, bigBlastFX;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            var angle = Vector2.Angle(hoopController.GetHoopDirection(), other.GetComponent<Rigidbody2D>().velocity);
            Debug.Log($"Angle: {angle}");
            if(angle >= 90f)
            {
                Debug.LogError("Lose");
                GameController.Instance.DeactivePerfectForm();
                GameManager.Instance.ChangePhase(GameState.OnDeath);
                return;
            }
            //if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y != 0)
            //{
            if (!hoopController.BorderInteract)
            {
                GameController.Instance.IncreaseSwitch();
                starFX.Play();
                if(GameController.Instance.Swish > 2)
                {
                    bigSmokeFX.Play();
                    blastFX.Play();
                    bigBlastFX.Play();
                }
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
            //}
            //else
            //{
            //    Debug.LogError("Lose");
            //    GameController.Instance.DeactivePerfectForm();
            //    GameManager.Instance.ChangePhase(GameState.OnDeath);
            //}
        }
    }
}