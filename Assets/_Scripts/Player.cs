using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private float _gravityScale;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        _gravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(0, 0);
            Jump();
            // rb.angularVelocity *= 0.7f;
        }
        // if(rb.velocity.y < 0)
        // {
        //     Debug.LogError("Stop");
        //     Debug.Break();
        // }
        Logger.Log($"Velocity: {rb.velocity}");
        // else rb.velocity *= 0.95f;
    }

    private void Jump()
    {
        rb.velocity = direction * speed;
        rb.gravityScale = _gravityScale;
        animator.Play("Flap", 0, 0);
        SoundManager.Instance.PlaySound(SoundManager.Sound.flap);
        // Debug.Log($"Angle of z: {transform.localRotation.eulerAngles.z}");
        // int angle = (int)transform.localRotation.eulerAngles.z;
        // // if(!GameManager.Instance.GameStart) GameManager.Instance.GameStart = true;
        // if(angle >= 0 && angle % 360 >= 45) return;
        // if(angle < 0 && angle % 360 >= -315) return;
        // angle += 5;
        // transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    
}
