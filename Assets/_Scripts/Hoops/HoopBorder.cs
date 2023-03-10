using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopBorder : MonoBehaviour
{
    [SerializeField] private HoopChecker hoopChecker;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            hoopChecker.BorderInteract = true;
            GameController.Instance.Swish = 0;
        }
        // else if(this.name.Equals("HoopBack"))
        // {
        //     hoopChecker.BorderInteract = true;
        //     GameController.Instance.Swish = 0;
        // }
        // if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        // {
        // }
        if (other.relativeVelocity.magnitude > 2f)
        {
            Vector2 dir = new Vector2(0.5f, other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            // Vector2 dir = new Vector2(0.3f * other.gameObject.GetComponent<Rigidbody2D>().velocity.x, other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = dir;
        }
        if (other.relativeVelocity.magnitude > 1f)
            SoundManager.Instance.PlaySound(Sound.bounce);
    }
}