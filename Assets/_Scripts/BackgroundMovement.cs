using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private Transform bg1, bg2;
    private float distance;
    public static BackgroundMovement Instance;

    private void Awake() 
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        distance = bg2.position.x - bg1.position.x;
    }

    private void Start()
    {
        ResetPosition();
    }

    private void Update() 
    {
        if(bg1.position.x < Camera.main.transform.position.x - 18f)
        {
            bg1.Translate(bg2.position.x + distance - bg1.position.x, 0, 0);
        }
        if(bg2.position.x < Camera.main.transform.position.x - 18f)
        {
            bg2.Translate(bg1.position.x + distance - bg2.position.x, 0, 0);
        }
    }

    private void ResetPosition()
    {
        bg1.position = Vector3.zero;
        bg2.position = bg1.position + Vector3.right * distance;
    }
}
