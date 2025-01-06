
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Camera minimapCamera;

    private PostProcessingTrigger postProcessingTrigger;
    private Ray mainCameraRay;
    [SerializeField] private LayerMask CullingTarget;
    private Collider[] colliders;

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

        mainCameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(mainCameraRay, out RaycastHit hit, 5.0f, CullingTarget))
        {
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();

            if (renderer.enabled)
            {
                renderer.enabled = false;
                StartCoroutine(ActiveObject(hit));
            }
        }
    }

    IEnumerator ActiveObject(RaycastHit hitObj)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1.0f);

            mainCameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(mainCameraRay, out RaycastHit hit, 5.0f, CullingTarget))
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        mainCameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (mainCameraRay.direction.normalized * 5.0f));
    }
}

