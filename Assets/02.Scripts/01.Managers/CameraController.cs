using System;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Camera minimapCamera;

    public void Initialize()
    {
        virtualCamera.Follow = GameManager.Instance._player.CamarePivot.transform;
        virtualCamera.LookAt = GameManager.Instance._player.transform;
    }

    public void ToggleFollowTarget(Transform newFollowTr , float closeUpTime)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = newFollowTr;
            virtualCamera.LookAt = newFollowTr;

            Invoke("ResetFollowTarget", closeUpTime);
        }
    }

    public void ResetFollowTarget()
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = GameManager.Instance._player.CamarePivot.transform;
            virtualCamera.LookAt = GameManager.Instance._player.transform;
            GameManager.Instance._player.enabled = true;
        }
    }
}

