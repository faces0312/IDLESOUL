using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _position;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        //Debug - 미니맵 카메라 추가시 연결 할것 
        //_position = new Vector3(player.transform.position.x,_camera.transform.position.y , player.transform.position.z);
        //_camera.transform.position = _position;
    }
}
