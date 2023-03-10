using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HoopChecker : MonoBehaviour
{
    [SerializeField] private GameObject ring, axis, entireHoop;
    // [SerializeField] private Collider2D frontHoopCollider, backHoopCollider, holeCollider, barrierCollider;
    [SerializeField] private List<Collider2D> listCollider;
    [SerializeField] private SpriteRenderer frontHoopSR, backHoopSR, axisSR;
    [SerializeField] private List<Quaternion> rotations;
    [SerializeField] private List<Vector3> scales;

    // private Vector3 moveSpeed, initScale;
    private bool borderInteract, passOver, isMovable;
    [SerializeField] private float moveSpeed;

    public bool BorderInteract { get => borderInteract; set => borderInteract = value; }
    public bool PassOver { get => passOver; set => passOver = value; }

    private void OnEnable() 
    {
        borderInteract = false;
        passOver = false;
        this.transform.position = new Vector3(HoopManager.Instance.GetHorizontalPosition(), HoopManager.Instance.GetVerticalPosition(), 0);
        DeactiveColor();
        GetTypeOfHoop();
        // auto arrange position when spawn
    }

    private void Update() 
    {
        if(!isMovable) return;
        if(ring.transform.localPosition.y >= 0.8f || ring.transform.localPosition.y <= -0.8f) moveSpeed *= -1;
        ring.transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
    }

    public void PassPoint()
    {
        isMovable = false;
        // gameObject.SetActive(false);
        ring.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        }
        );
        Fade(0, 1);
        foreach(var collider in listCollider)
        {
            collider.enabled = false;
        }
        HoopManager.Instance.GetReadyHoop().SetActive(true);
    }

    public void Fade(float endValue, float time)
    {
        axisSR.DOKill();
        frontHoopSR.DOKill();
        backHoopSR.DOKill();
        frontHoopSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        backHoopSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        axisSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true); ;
    }

    public void ActiveColor(bool fade = true) //
    {
        if(fade) Fade(1, 0.001f);

        foreach(var collider in listCollider)
        {
            collider.enabled = true;
        }
    }
    public void DeactiveColor(bool fade = true) //
    {
        if(fade) Fade(0.5f, 0.001f);

        foreach(var collider in listCollider)
        {
            collider.enabled = false;
        }
    }

    private void GetTypeOfHoop()
    {
        int score = GameController.Instance.Score;
        if(score < 20)
        {
            isMovable = false;
            ring.transform.localScale = scales[1];
            entireHoop.transform.localRotation = rotations[0];
        }
        else if(score <= 60)
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
