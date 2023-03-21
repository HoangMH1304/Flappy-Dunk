using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HoopController : MonoBehaviour
{
    [SerializeField] private GameObject ring, axis, entireHoop;
    [SerializeField] private List<Collider2D> listCollider;
    [SerializeField] private List<SpriteRenderer> listEffectSR;
    [SerializeField] private List<Quaternion> rotations;
    [SerializeField] private List<Vector3> scales;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMovable;
    [SerializeField] private bool borderInteract, passOver;
    public bool BorderInteract { get => borderInteract; set => borderInteract = value; }
    public bool PassOver { get => passOver; set => passOver = value; }
    public bool IsMovable { get => isMovable; set => isMovable = value; }

    private void OnEnable()
    {
        InitialSpecs();
        Debug.Log("Enable hoop", gameObject);
    }

    public void InitialSpecs()
    {
        ring.transform.DORewind();
        InitState();
        DeactiveState();
        if (GameManager.Instance.IsEndlessMode)
        {
            transform.position = new Vector3(HoopManager.Instance.GetHorizontalPosition(), HoopManager.Instance.GetVerticalPosition(), 0);
            GetTypeOfHoop();
        }
        else
        {
            if (isMovable) axis.SetActive(true);
        }
    }

    public void InitState()
    {
        borderInteract = false;
        passOver = false;
    }

    private void Update() 
    {
        if(!isMovable) return;
        if(ring.transform.localPosition.y >= 0.8f || ring.transform.localPosition.y <= -0.8f) moveSpeed *= -1;
        ring.transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
    }

    public void PassPoint()
    {
        ring.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            ring.transform.DOScale(new Vector3(1, 1, 1), 0).SetUpdate(true);
        }
        );
        Fade(0, 1);
        foreach (var collider in listCollider)
        {
            collider.enabled = false;
        }
        // turn off collider of hole and back restrict range only
        HoopManager.Instance.GetReadyHoop()?.SetActive(true);
    }

    public void Fade(float endValue, float time)
    {
        foreach(var spriteRenderer in listEffectSR)
        {
            spriteRenderer.DOKill();
            spriteRenderer.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        }
        // axisSR.DOKill();
        // frontHoopSR.DOKill();
        // backHoopSR.DOKill();
        // frontHoopSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        // backHoopSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        // axisSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true); ;
    }

    public void ActiveState(bool fade = true) //
    {
        if(fade) Fade(1, 0.001f);

        foreach(var collider in listCollider)
        {
            collider.enabled = true;
        }
    }
    public void DeactiveState(bool fade = true) //
    {
        if(fade) Fade(0.5f, 0.001f);
        //turn off collider of hole and restrict range only
        for(int i = 2; i < listCollider.Count; i++)
        {
            listCollider[i].enabled = false;
        }
    }

    private void TurnIntoNormalHoop()
    {
        isMovable = false;
        ring.transform.localScale = scales[0];
        entireHoop.transform.localRotation = rotations[0];
        axis.SetActive(false);
        Debug.Log("Turn into normal", gameObject);
        Debug.Log($"Score: {GameController.Instance.Score}");
    }

    private void GetTypeOfHoop()
    {
        TurnIntoNormalHoop();
        int score = GameController.Instance.Score;
        if(score < 20)
        {
            isMovable = false;
            ring.transform.localScale = scales[1];
            entireHoop.transform.localRotation = rotations[0];
        }
        else if(score <= 50)
        {
            isMovable = (Random.Range(1, 100) <= 30) ? true : false;  //30%
            ring.transform.localScale = scales[(Random.Range(1, 100) <= 70) ? 1 : 0];
            if(isMovable)
            {
                entireHoop.transform.localRotation = rotations[(Random.Range(1, 100) <= 70) ? 1 : 0];
            }
            else
            {
                entireHoop.transform.localRotation = rotations[Random.Range(0, 3)];
            }
        }
        else
        {
            isMovable = (Random.Range(1, 100) <= 70) ? true : false;
            ring.transform.localScale = scales[(Random.Range(1, 100) <= 60) ? 0 : 1];
            entireHoop.transform.localRotation = rotations[Random.Range(0, 5)];
        }
        if(isMovable) axis.SetActive(true);
        else axis.SetActive(false);
    }
}
