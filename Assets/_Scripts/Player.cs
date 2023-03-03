using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !IsMouseOverUI())
        {
            // rb.velocity = new Vector2(0, 0);
                Jump();
            // rb.angularVelocity *= 0.7f;
        }
    }

    private void Jump()
    {
        rb.velocity = direction * speed;
        rb.gravityScale = _gravityScale;
        animator.Play("Flap", 0, 0);
        SoundManager.Instance.PlaySound(SoundManager.Sound.flap);
    }

    public bool IsMouseOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("TOUCH: UI (1)");
            return true;
        }

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                Debug.Log("TOUCH: UI (2)");
                return true;
            }
        }
        Debug.Log("TOUCH OBJ");
        return false;
    }
}
