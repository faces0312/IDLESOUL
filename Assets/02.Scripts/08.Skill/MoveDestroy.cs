using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MoveDestroy : MonoBehaviour
{
    public GameObject gameObjectMain;
    public GameObject gameObjectTail;
    private GameObject makedObject;

    public GameObject hitObject;

    public LayerMask layerMask;

    public float maxLength;

    public bool isDestroy;
    public float objectDestroyTime;
    public float tailDestroyTime;
    public float hitObjectDestroyTime;

    public float maxTime = 1;
    public float moveSpeed = 10;

    public bool isCheckHitTag;
    public string targetTag;

    public bool isShieldActive = false;
    public bool isHitMake = true;

    private float time;
    private bool isHit;
    //private float scalefactor;

    public GameObject MakedObject { get => makedObject; }

    private void Start()
    {
        //scalefactor = transform.parent.localScale.x;
        time = Time.time;
    }

    void LateUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed/* * scalefactor*/);
        if (!isHit)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength, layerMask))
                HitObj(hit);
        }

        if (isDestroy)
        {
            if (Time.time > time + objectDestroyTime)
            {
                MakeHitObject(transform);
                Destroy(gameObject);
            }
        }
    }

    void MakeHitObject(RaycastHit hit)
    {
        if (isHitMake == false)
            return;
        makedObject = Instantiate(hitObject, hit.point, Quaternion.LookRotation(hit.normal));
        //makedObject.transform.parent = transform.parent;
        makedObject.transform.localScale = new Vector3(1, 1, 1);
    }

    void MakeHitObject(Transform point)
    {
        if (isHitMake == false)
            return;
        makedObject = Instantiate(hitObject, point.transform.position, point.rotation);
        //makedObject.transform.parent = transform.parent;
        makedObject.transform.localScale = new Vector3(1, 1, 1);
    }

    void HitObj(RaycastHit hit)
    {
        if (isCheckHitTag)
            if (hit.transform.tag != targetTag)
                return;
        isHit = true;
        if (gameObjectTail)
            gameObjectTail.transform.parent = null;
        MakeHitObject(hit);

        if (isShieldActive)
        {
            ShieldActivate m_sc = hit.transform.GetComponent<ShieldActivate>();
            if (m_sc)
                m_sc.AddHitObject(hit.point);
        }

        Destroy(this.gameObject);
        Destroy(gameObjectTail, tailDestroyTime);
        Destroy(makedObject, hitObjectDestroyTime);
    }
}
