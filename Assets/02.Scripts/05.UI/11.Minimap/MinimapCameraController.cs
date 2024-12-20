using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _position;
    private Transform playerTr;

    void Start()
    {
        _camera = GetComponent<Camera>();
        playerTr = GameManager.Instance.player.transform;
    }

    private void FixedUpdate()
    {
        //Debug - 미니맵 카메라 추가시 연결 할것 
        _position = new Vector3(playerTr.position.x,_camera.transform.position.y , playerTr.position.z);
        _camera.transform.position = _position;
    }
}
