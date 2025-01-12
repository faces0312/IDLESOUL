
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor.PackageManager;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Camera minimapCamera;

    private PostProcessingTrigger postProcessingTrigger;
    private Ray mainCameraRay;
    [SerializeField] private LayerMask CullingTarget;
    private Collider[] colliders;
    private float radius = 3.0f;
    private float maxDistance = 3.0f;

    private Queue<GameObject> DisableObjects = new Queue<GameObject>();

    public CinemachineVirtualCamera VirtualCamera { get => virtualCamera; }
    public Camera MinimapCamera { get => minimapCamera; }

    private void Awake()
    {
        // PostProcessing
        if (Camera.main.TryGetComponent(out PostProcessVolume volume))
        {
            postProcessingTrigger = new PostProcessingTrigger(volume);
        }
    }

    public void Initialize(Transform Follow, Transform LookAt)
    {
        virtualCamera.Follow = Follow;
        virtualCamera.LookAt = LookAt;

        // 포스트 프로세스 세팅
        if (Camera.main.TryGetComponent(out PostProcessVolume volume))
        {
            postProcessingTrigger = new PostProcessingTrigger(volume);
        }
    }

    public void ToggleFollowTarget(Transform newFollowTr, float closeUpTime)
    {
        UIManager.Instance.ShowUI<UIBossSummonAlarmController>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = newFollowTr;
            virtualCamera.LookAt = newFollowTr;

            Invoke("ResetFollowTarget", closeUpTime);
        }
    }

    public void ResetFollowTarget()
    {
        UIManager.Instance.HideUI<UIBossSummonAlarmController>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = GameManager.Instance.player.CamarePivot.transform;
            virtualCamera.LookAt = GameManager.Instance.player.transform;
            GameManager.Instance.player.enabled = true;
        }
    }

    public void ShakeCamera(CinemachineImpulseSource source)
    {
        source.GenerateImpulse();
    }

    public void SwordSlashEffect()
    {
        postProcessingTrigger.SwordSlashEffect();
    }

    public void MeteorEffect()
    {
        postProcessingTrigger.MeteorEffect();
    }

    public void LateUpdate()
    {
        //colliders = Physics.OverlapSphere(transform.position, 5.0f, CullingTarget);

        //foreach (Collider col in colliders)
        //{
        //    MeshRenderer renderer = col.GetComponent<MeshRenderer>();

        //    if (renderer.enabled)
        //    {
        //        renderer.enabled = false;
        //        StartCoroutine(ActiveObject(col));
        //    }
        //}

        //mainCameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //if (Physics.Raycast(mainCameraRay, out RaycastHit hit, 5.0f, CullingTarget))
        //{
        //    MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();

        //    if (renderer.enabled)
        //    {
        //        renderer.enabled = false;
        //        StartCoroutine(ActiveObject(hit));
        //    }
        //}

      
        if (Physics.SphereCast(Camera.main.transform.position, radius, Camera.main.transform.forward,out RaycastHit hit, maxDistance, CullingTarget))
        {
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();

            if (renderer.enabled)
            {
                renderer.enabled = false;
                StartCoroutine(ActiveObject(hit));
            }
        }
    }

    //IEnumerator ActiveObject(RaycastHit hitObj)
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        yield return new WaitForSeconds(1.0f);

    //        mainCameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

    //        if (Physics.Raycast(mainCameraRay, out RaycastHit hit, 5.0f, CullingTarget))
    //        {
    //            if (hitObj.collider.gameObject != hit.collider.gameObject)
    //            {
    //                hitObj.collider.GetComponent<MeshRenderer>().enabled = true;
    //                yield break;
    //            }
    //        }
    //    }

    //    hitObj.collider.GetComponent<MeshRenderer>().enabled = true;
    //}

    IEnumerator ActiveObject(RaycastHit hitObj)
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1.0f);

            if (Physics.SphereCast(Camera.main.transform.position, radius, Camera.main.transform.forward, out RaycastHit hit, maxDistance, CullingTarget))
            {
                if (hitObj.collider.gameObject != hit.collider.gameObject)
                {
                    hitObj.collider.GetComponent<MeshRenderer>().enabled = true;
                    yield break;
                }
            }
        }

        hitObj.collider.GetComponent<MeshRenderer>().enabled = true;
    }

    //IEnumerator ActiveObject(Collider hitObj)
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        yield return new WaitForSeconds(1.0f);

    //        colliders = Physics.OverlapSphere(transform.position, 5.0f, CullingTarget);

    //        foreach (Collider col in colliders)
    //        {
    //            MeshRenderer renderer = col.GetComponent<MeshRenderer>();

    //            if (hitObj.gameObject != col.gameObject)
    //            {
    //                hitObj.GetComponent<MeshRenderer>().enabled = true;
    //                yield break;
    //            }
    //        }

    //    }

    //    hitObj.GetComponent<MeshRenderer>().enabled = true;
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    mainCameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //    Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (mainCameraRay.direction.normalized * 5.0f));
    //}

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, radius);

        // 카메라 위치와 방향 설정
        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        // SphereCast 시각화
        if (Physics.SphereCast(origin, radius, direction, out RaycastHit hitInfo, maxDistance, CullingTarget))
        {
            // 충돌된 지점 표시
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitInfo.point, radius);

            // 구체와 충돌 지점까지의 라인 그리기
            Gizmos.DrawLine(origin, hitInfo.point);
        }
        else
        {
            // 최대 거리까지 구체 라인 그리기
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(origin, direction * maxDistance);
        }

        // 시작 지점 구체 표시
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, radius);
    }
}

