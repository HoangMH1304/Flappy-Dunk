using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopBorder : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.relativeVelocity.magnitude > 2f)
        {
            Vector2 dir = new Vector2(0.3f * other.gameObject.GetComponent<Rigidbody2D>().velocity.x, other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            other.gameObject.GetComponent<Rigidbody2D>().velocity= dir;
        }
        if (other.relativeVelocity.magnitude > 1f)
            SoundManager.Instance.PlaySound(SoundManager.Sound.bounce);
    }
}