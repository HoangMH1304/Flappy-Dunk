using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance;

    private void Update() 
    {
        // if(GameManager.Instance.GameOver) return;
        transform.position = new Vector3(target.position.x + distance, transform.position.y, transform.position.z);
    }
}
