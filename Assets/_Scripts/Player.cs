using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = direction * speed;
        rb.gravityScale = 1f;
        if(!GameManager.Instance.GameStart) GameManager.Instance.GameStart = true;
        SoundManager.Instance.PlaySound(SoundManager.Sound.flap);
    }
}
