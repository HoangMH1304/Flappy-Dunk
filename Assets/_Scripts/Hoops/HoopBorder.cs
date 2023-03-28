using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopBorder : MonoBehaviour
{
    [SerializeField] private HoopController hoopController;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hoopController.PassOver)
        {
            hoopController.BorderInteract = true;
            GameController.Instance.SwishStreak = 0;
            GameController.Instance.DeactivePerfectForm();
        }
        if (other.relativeVelocity.magnitude > 2f)
        {
            Vector2 dir = new Vector2(0.5f, other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = dir;
        }
        if (other.relativeVelocity.magnitude > 1f)
            SoundManager.Instance.PlaySound(Sound.bounce);
    }
}