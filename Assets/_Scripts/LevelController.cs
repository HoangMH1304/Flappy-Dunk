using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private HoopManager hoopManager;
    [SerializeField] private Goal goal;

    private void OnEnable()
    {
        hoopManager.InitialInChallenge();
        goal.InitialSpecs();
    }
}
