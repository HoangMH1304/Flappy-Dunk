using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    [SerializeField] private GameObject followObject;
    [SerializeField] private Animator shake;
    [SerializeField] private GameObject lobbyCanvas;
    private float smoothSpeed;
    private bool flashToBall, slowable;  //follow the ball when revive
    private void Awake()
    {
        Instance = this;
    }
    public void Shake()
    {
        shake.SetTrigger("Shake");
    }
    void Start()
    {
        slowable = false;
        flashToBall = false;
    }
    //public void AssignFollowObject()
    //{
    //    InitialCameraPosition();
    //}
    public void MoveToBall()
    {
        flashToBall = true;
        transform.DOKill();
        transform.DOMoveX(followObject.transform.position.x + 1.5f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(()=>
        {
            flashToBall = false;
        });
    }
    public void InitialCameraPosition()
    {
        transform.position = new Vector3(followObject.transform.position.x + 1.5f, transform.position.y, transform.position.z);
        slowable = false;
        // lobbyCanvas.transform.position = new Vector3(followObject.transform.position.x + 1.5f, transform.position.y, transform.position.z);
    }

    //public void GetLast
    void Update()
    {
        if (flashToBall) return;
        if (GameManager.Instance.Playable)
        {
            transform.position = new Vector3(followObject.transform.position.x + 1.5f, transform.position.y, transform.position.z);
            //Debug.Log($"transform.position = {transform.position}");
            slowable = false;
        }
        else
        {
            if (slowable == false)
            {
                slowable = true;
                smoothSpeed = followObject.GetComponent<Rigidbody2D>().velocity.x;
                //if (GameManager.mode == GameMode.Challenge)
                if (smoothSpeed > 1)
                    smoothSpeed = 1.0f;
            }
            if (smoothSpeed > 0)  //slowdown until stop
            {
                transform.Translate(smoothSpeed * Time.deltaTime, 0, 0);
                smoothSpeed -= 0.01f;
            }
        }
    }
}
