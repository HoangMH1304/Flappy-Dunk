using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.SceneManagement;


public class TestVibrate : MonoBehaviour
{
    public void LightImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact, true);
    }

    public void Failure()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure, true);

    }

    public void HeavyImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact, true);

    }

    public void MediumImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact, true);

    }

    public void RigidImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.RigidImpact, true);

    }

    public void Selection()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection, true);
    }

    public void SoftImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.SoftImpact, true);

    }

    public void Success()
    {
        MMVibrationManager.Haptic(HapticTypes.Success, true);

    }

    public void Warning()
    {
        MMVibrationManager.Haptic(HapticTypes.Warning, true);

    }

    public void Exit()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);

    }
}
