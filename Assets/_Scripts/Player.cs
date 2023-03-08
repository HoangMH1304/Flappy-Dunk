using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private float _gravityScale;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject frontWing, backWing;
    // [SerializeField] private GameObject lobby;
    // [SerializeField] private GameObject gameplay;
    // [SerializeField] private GameObject hoopContainer;
    private Rigidbody2D rb;
    private Vector3 initialPosition = new Vector3(-1.5f, 0, 0);

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        _gravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    private void OnEnable() 
    {
        transform.position = initialPosition;  
    }

    private void Update() 
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        && !IsMouseOverUI() && GameManager.Instance.Playable)
        {
            // rb.velocity = new Vector2(0, 0);
            Jump();
            // rb.angularVelocity *= 0.7f;
        }
    }

    public void Jump()
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
            // Debug.Log("TOUCH: UI (1)");
            return true;
        }
        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                // Debug.Log("TOUCH: UI (2)");
                return true;
            }
        }
        // Debug.Log("TOUCH OBJ");
        return false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ceil") || other.gameObject.CompareTag("Floor"))
        {
            Logger.Log("Game over");
            GameManager.Instance.ChangeState(GameState.SecondChance);
            // StartCoroutine(ReturnLobby());
        }   
    }

    public void Fade(float endValue, float time)
    {
        frontWing.GetComponent<SpriteRenderer>().DOKill();
        backWing.GetComponent<SpriteRenderer>().DOKill();
        gameObject.GetComponent<SpriteRenderer>().DOKill();
        frontWing.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
        backWing.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
        gameObject.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
    }
}
