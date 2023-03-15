using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    [SerializeField] GameObject followObject;
    [SerializeField] Animator shake;
    [SerializeField] GameObject lobbyCanvas;
    float smoothSpeed;
    bool flag;
    public bool isMovingToBall;
    private void Awake()
    {
        Instance = this;
    }
    public void Shake()
    {
        // shake.Play("Shake");
        shake.SetTrigger("Shake");
    }
    void Start()
    {
        flag = false;
        isMovingToBall = false;
    }
    public void AssignFollowObject()
    {
        followObject = GameObject.FindGameObjectWithTag("Player");
        Reset();
    }
    public void MoveToBall()
    {
        isMovingToBall = true;
        transform.DOKill();
        transform.DOMoveX(followObject.transform.position.x + 1.5f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(()=>
        {
            isMovingToBall = false;
        });
    }
    public void Reset()
    {
        transform.position = new Vector3(followObject.transform.position.x + 1.5f, transform.position.y, transform.position.z);
        // lobbyCanvas.transform.position = new Vector3(followObject.transform.position.x + 1.5f, transform.position.y, transform.position.z);
        flag = false;
    }
    void Update()
    {
        if (isMovingToBall == false)
            if (GetGameOverState() == false)
            {
                transform.position = new Vector3(followObject.transform.position.x + 1.5f, transform.position.y, transform.position.z);
                flag = false;
            }
            else
            {
                if (flag == false)
                {
                    flag = true;
                    smoothSpeed = followObject.GetComponent<Rigidbody2D>().velocity.x;
                    //if (GameManager.mode == GameMode.Challenge)
                    if (smoothSpeed > 1)
                        smoothSpeed = 1.0f;
                }
                if (smoothSpeed > 0)
                {
                    transform.Translate(smoothSpeed * Time.deltaTime, 0, 0);
                    smoothSpeed -= 0.005f;
                }
            }
    }

    private bool GetGameOverState()
    {
        return !GameManager.Instance.Playable;
        // if (GameManager.mode == GameMode.Challenge)
        // {
        //     return ChallengeController.Instance.IsGameOver;
        // }
        // else return GameController.instance.IsGameOver;
    }
}
