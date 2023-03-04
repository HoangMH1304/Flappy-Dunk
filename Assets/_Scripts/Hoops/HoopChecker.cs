using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopChecker : MonoBehaviour
{
    [SerializeField] private GameObject ring, axis;
    [SerializeField] private Collider2D frontHoopCollider, backHoopCollider, holeCollider;
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
        GetTypeOfHoop();
        // auto arrange position when spawn
    }

    private void Update() 
    {
        if(!isMovable) return;
        if(ring.transform.localPosition.y >= 0.8f || ring.transform.localPosition.y <= -0.8f) moveSpeed *= -1;
        ring.transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
    }

    public void SwitchColliderState(bool state)
    {
        frontHoopCollider.enabled = state;
        backHoopCollider.enabled = state;
        holeCollider.enabled = state;
    }

    public void PassPoint()
    {
        isMovable = false;
        gameObject.SetActive(false);
        SwitchColliderState(false);
        HoopManager.Instance.GetReadyHoop().SetActive(true);
    }

    private void GetTypeOfHoop()
    {
        int score = ScoreManager.Instance.Score;
        if(score < 20)
        {
            isMovable = false;
            ring.transform.localScale = scales[1];
            transform.localRotation = rotations[0];
        }
        else if(score <= 60)
        {
            isMovable = (Random.Range(1, 100) <= 100) ? true : false;  //30%
            ring.transform.localScale = scales[(Random.Range(1, 100) <= 70) ? 1 : 0];
            if(isMovable)
            {
                transform.localRotation = rotations[(Random.Range(1, 100) <= 70) ? 1 : 0];
            }
            else
            {
                transform.localRotation = rotations[Random.Range(0, 3)];
            }
        }
        else
        {
            isMovable = (Random.Range(1, 100) <= 70) ? true : false;
            ring.transform.localScale = scales[(Random.Range(1, 100) <= 60) ? 0 : 1];
            transform.localRotation = rotations[Random.Range(0, 5)];
        }
        if(isMovable) axis.SetActive(true);
        else axis.SetActive(false);
    }
}
