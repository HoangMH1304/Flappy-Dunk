using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject frontWing, backWing;
    private Rigidbody2D rb;
    private bool onGround;
    private Vector3 initialPosition = new Vector3(-1.5f, 0, 0);
    private Vector3 initBackWingTransform = new Vector3(0.65f, 0.6f, 0);
    private Vector3 initFrontWingTransform = new Vector3(-0.95f, 0.6f, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        transform.position = initialPosition;
        onGround = false;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        && !IsMouseOverUI() && GameManager.Instance.Playable)
        {
            Time.timeScale = 1;
            Jump();
            // rb.angularVelocity *= 0.7f;
        }
    }

    public void Jump()
    {
        rb.velocity = direction * speed;
        animator.Play("Flap", 0, 0);
        SoundManager.Instance.PlaySound(Sound.flap);
    }

    private void wingFall(string tag)
    {
        frontWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        backWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        SoundManager.Instance.PlaySound(Sound.crash);
        if (tag == "Ceil")
        {
            frontWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, -10), 0), ForceMode2D.Force);
            backWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(10, 80), 0), ForceMode2D.Force);
        }
        if (tag == "Floor")
        {
            frontWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, -50), Random.Range(400, 500)), ForceMode2D.Force);
            backWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(50, 80), Random.Range(400, 500)), ForceMode2D.Force);
        }
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
        if(other.gameObject.CompareTag("Ceil"))
        {
            GameManager.Instance.ChangeState(GameState.OnDeath);
            wingFall("Ceil");
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            if(!onGround)
            {
                onGround = true;
                Logger.Log("Detect ground");
                wingFall("Floor");
            }
            else SoundManager.Instance.PlaySound(Sound.bounce);
            GameManager.Instance.ChangeState(GameState.OnDeath);
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

    public void Reset()
    {
        //ball
        Time.timeScale = 0;
        transform.position = new Vector3(-1.5f, 0.315f, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rb.velocity = new Vector2(0, 0);
        rb.angularDrag = 1;
        rb.angularVelocity = 0;

        //wing
        frontWing.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        backWing.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        frontWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        backWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        frontWing.transform.localPosition = initFrontWingTransform;
        backWing.transform.localPosition = initBackWingTransform;
        frontWing.transform.rotation = Quaternion.identity;
        backWing.transform.rotation = Quaternion.identity;

        // Fade(1, 2f);
        onGround = false;
    }
}
