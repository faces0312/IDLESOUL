using System;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Camera minimapCamera;

    public CinemachineVirtualCamera VirtualCamera { get => virtualCamera; }
    public Camera MinimapCamera { get => minimapCamera; }

    public void SetMininapCamera()
    {

    }

    public void Initialize(Transform Follow , Transform LookAt)
    {
        //virtualCamera.Follow = GameManager.Instance._player.CamarePivot.transform;
        //virtualCamera.LookAt = GameManager.Instance._player.transform;
        
        virtualCamera.Follow = Follow;
        virtualCamera.LookAt = LookAt;
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

    public void ResetFollowTarget(Transform Follow, Transform LookAt)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = Follow;
            virtualCamera.LookAt = LookAt;
            GameManager.Instance.player.enabled = true;
        }
    }
}

