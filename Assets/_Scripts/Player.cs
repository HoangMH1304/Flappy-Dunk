using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private const string CEIL = "Ceil";
    private const string FLOOR = "Floor";
    private const string FLAP = "Flap";
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject frontWing, backWing, perfectForm;
    [SerializeField] private SpriteRenderer blackBall, blackFrontWing, blackBackWing;
    [SerializeField] private ParticleSystem smoke, flame;
    private Rigidbody2D rb;
    private bool onGround = false;
    private Vector3 initialFrontWingTransform = new Vector3(-0.875f, 0.8f, 0);
    private Vector3 initialBackWingTransform = new Vector3(0.6f, 0.9f, 0);
    public Vector2 Direction { get => direction; set => direction = value; }

    private void OnEnable() {
        if(rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        && !IsMouseOverUI() && GameManager.Instance.Playable)
        {
            Jump();
            // rb.angularVelocity *= 0.7f;
        }
    }

    public void Jump()
    {
        Time.timeScale = 1;
        rb.velocity = direction * speed;
        animator.Play(FLAP, 0, 0);
        SoundManager.Instance.PlaySound(Sound.flap);
    }

    private void FallenWing(string tag)
    {
        frontWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        backWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        SoundManager.Instance.PlaySound(Sound.crash);
        if (tag == CEIL)
        {
            frontWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, -10), 0), ForceMode2D.Force);
            backWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(10, 80), 0), ForceMode2D.Force);
        }
        if (tag == FLOOR)
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
        if(other.gameObject.CompareTag(CEIL))
        {
            GameManager.Instance.ChangePhase(GameState.OnDeath);
            FallenWing(CEIL);
            DeactivatePerfectForm();
        }

        if (other.gameObject.CompareTag(FLOOR))
        {
            if(!onGround)
            {
                onGround = true;
                Logger.Log("Detect ground");
                FallenWing(FLOOR);
                DeactivatePerfectForm();
            }
            else SoundManager.Instance.PlaySound(Sound.bounce);
            GameManager.Instance.ChangePhase(GameState.OnDeath);
        }
    }

    public void FadeCharacter(float endValue, float time)
    {
        frontWing.GetComponent<SpriteRenderer>().DOKill();
        backWing.GetComponent<SpriteRenderer>().DOKill();
        gameObject.GetComponent<SpriteRenderer>().DOKill();
        frontWing.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
        backWing.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
        gameObject.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
    }

    public void RestoreInitialState()
    {
        onGround = false;
        Time.timeScale = 0;
        
        //ball
        transform.position = new Vector3(-1.5f, 0.315f, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rb.velocity = new Vector2(0, 0);
        rb.angularDrag = 1.5f;
        rb.angularVelocity = 0;

        //wing
        frontWing.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        backWing.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        frontWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        backWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        frontWing.transform.localPosition = initialFrontWingTransform;
        backWing.transform.localPosition = initialBackWingTransform;
        frontWing.transform.rotation = Quaternion.identity;
        backWing.transform.rotation = Quaternion.identity;

        // Fade(1, 2f);
    }

    private void ActivePerfectSprite()
    {
        perfectForm.SetActive(true);
        blackBall.DOFade(1, 0.01f);
        blackFrontWing.DOFade(1, 0.01f);
        blackBackWing.DOFade(1, 0.01f);
    }

    private void DeactivePerfectSprite()
    {
        blackFrontWing.DOFade(0, 0.5f);
        blackBackWing.DOFade(0, 0.5f);
        blackBall.DOFade(0, 0.5f).OnComplete(() => perfectForm.SetActive(false));
    }

    public void ActivePerfectForm(int swish)
    {
        if(swish >= 2)
        {
            ActivePerfectSprite();
            smoke.Stop();
            flame.Play();
        }
        else if(swish == 1)
        {
            DeactivePerfectSprite();
            smoke.Play();
            flame.Stop();
        }
        else
        {
            DeactivePerfectSprite();
            smoke.Stop();
            flame.Stop();
        }
    }

    public void DeactivatePerfectForm()
    {
        DeactivePerfectSprite();
        smoke.Stop();
        flame.Stop();
    }
}
