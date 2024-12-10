using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float speed = 15f; //투사체 속도
    [SerializeField] protected float hitOffset = 0f; //투사체 피격 오프셋 
    [SerializeField] protected bool UseFirePointRotation; //방향성 있는 투사체 각도 조정 bool 변수
    [SerializeField] protected Vector3 rotationOffset = new Vector3(0, 0, 0); //투사체 각도 오프셋
    [SerializeField] protected GameObject hit; // hit시 발생하는 이펙트 오브젝트
    [SerializeField] protected ParticleSystem hitPS;//hit시 발생하는 파티클시스템
    [SerializeField] protected GameObject flash; // 투사체의 Flash 효과
    [SerializeField] protected Rigidbody rb; 
    [SerializeField] protected Collider col;
    [SerializeField] protected Light lightSourse;
    [SerializeField] protected GameObject[] Detached; //해당 투사체에 있는 파티클 컨테이너
    [SerializeField] protected ParticleSystem projectilePS; //투사체 발사시 동작하는 파티클 시스템
    private bool startChecker = false; //투사체가 현재 사용중인지 체크하는 변수
    [SerializeField] protected bool notDestroy = false; //투사체가 파괴됬는지 체크하는 변수

    public float knockbackPower;//투사체 넉백 파워
    public Vector3 dir; //투사체가 발사되는 방향 
    public LayerMask TargetLayer; //해당 투사체를 맞추기 위한 타겟 레이어

    protected virtual void Start()
    {
        if (!startChecker)
        {
            /*lightSourse = GetComponent<Light>();
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            if (hit != null)
                hitPS = hit.GetComponent<ParticleSystem>();*/
            if (flash != null)
            {
                flash.transform.parent = null;
            }
        }
        if (notDestroy)
            StartCoroutine(DisableTimer(5));
        else
            Destroy(gameObject, 5);
        startChecker = true;
    }
    protected virtual IEnumerator DisableTimer(float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        yield break;
    }

    protected virtual void OnEnable()
    {
        if (startChecker)
        {
            if (flash != null)
            {
                flash.transform.parent = null;
            }
            if (lightSourse != null)
                lightSourse.enabled = true;
            col.enabled = true;
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = dir * speed;
        }
    }

    protected virtual void DamageCaculate(GameObject hitObject, float Damage)
    {
        ITakeDamageAble damageable = hitObject.GetComponent<ITakeDamageAble>();
        //TODO :: 무적시간이 아닐때에도 조건에 추가해야됨
        if (damageable != null)
        {
            damageable.TakeDamage(Damage);//매직넘버 (플레이어나 Enemy의 Stat값을 받아와서 적용 시켜야됨)

        }
    }

    protected virtual void KnockBackCaculate(GameObject hitObject, float Power)
    {
        ITakeDamageAble damageable = hitObject.GetComponent<ITakeDamageAble>();
        //TODO :: 무적시간이 아닐때에도 조건에 추가해야됨
        if (damageable != null)
        {

            Vector3 directionKnockBack = (hitObject.transform.position - transform.position).normalized;
            //damageable.TakeKnockBack(directionKnockBack, knockbackPower);
            directionKnockBack.y = 0; // y축 보정
            damageable.TakeKnockBack(directionKnockBack, Power);
        }
    }

    //protected void ProjectileCollison(Collision collision)
    //{
    //    //Lock all axes movement and rotation
    //    rb.constraints = RigidbodyConstraints.FreezeAll;
    //    //speed = 0;
    //    if (lightSourse != null)
    //        lightSourse.enabled = false;
    //    col.enabled = false;
    //    projectilePS.Stop();
    //    projectilePS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

    //    ContactPoint contact = collision.contacts[0];
    //    Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //    Vector3 pos = contact.point + contact.normal * hitOffset;

    //    //Spawn hit effect on collision
    //    if (hit != null)
    //    {
    //        hit.transform.rotation = rot;
    //        hit.transform.position = pos;
    //        if (UseFirePointRotation) { hit.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
    //        else if (rotationOffset != Vector3.zero) { hit.transform.rotation = Quaternion.Euler(rotationOffset); }
    //        else { hit.transform.LookAt(contact.point + contact.normal); }
    //        hitPS.Play();
    //    }

    //    //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
    //    foreach (var detachedPrefab in Detached)
    //    {
    //        if (detachedPrefab != null)
    //        {
    //            ParticleSystem detachedPS = detachedPrefab.GetComponent<ParticleSystem>();
    //            detachedPS.Stop();
    //        }
    //    }

    //    if (notDestroy)
    //        StartCoroutine(DisableTimer(hitPS.main.duration));
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }


    //    ObjectPoolManager.Instance.GetPool("playerProjectile", Utils.POOL_KEY_PLAYERPROJECTILE).GetObject();
    //}

    protected void ProjectileCollison(Collider other)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        //speed = 0;
        if (lightSourse != null)
            lightSourse.enabled = false;
        col.enabled = false;
        projectilePS.Stop();
        projectilePS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        Vector3 closetPoint = other.ClosestPoint(other.transform.position);

        //Spawn hit effect on collision
        if (hit != null)
        {
            //hit.transform.rotation = rot;
            hit.transform.position = closetPoint;
            if (UseFirePointRotation) { hit.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hit.transform.rotation = Quaternion.Euler(rotationOffset); }
            //else { hit.transform.LookAt(contact.point + contact.normal); }
            else { hit.transform.LookAt(closetPoint); }
            hitPS.Play();
        }

        //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                ParticleSystem detachedPS = detachedPrefab.GetComponent<ParticleSystem>();
                detachedPS.Stop();
            }
        }

        if (notDestroy)
            StartCoroutine(DisableTimer(hitPS.main.duration));
        else
        {
            gameObject.SetActive(false);
        }

    }

    protected void ProjectileMeleeCollison(Collider other)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        //speed = 0;
        if (lightSourse != null)
            lightSourse.enabled = false;
        col.enabled = false;
        //projectilePS.Stop();
        //projectilePS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        Vector3 closetPoint = other.ClosestPoint(other.transform.position);

        //Spawn hit effect on collision
        if (hit != null)
        {
            //hit.transform.rotation = rot;
            hit.transform.position = closetPoint;
            if (UseFirePointRotation) { hit.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hit.transform.rotation = Quaternion.Euler(rotationOffset); }
            //else { hit.transform.LookAt(contact.point + contact.normal); }
            else { hit.transform.LookAt(closetPoint); }
            hitPS.Play();
        }

        //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                ParticleSystem detachedPS = detachedPrefab.GetComponent<ParticleSystem>();
                detachedPS.Stop();
            }
        }

        if (gameObject.activeSelf == true)
            StartCoroutine(DisableTimer(projectilePS.main.duration));
    }

    protected void ProjectileRangeCollison(Collider other)
    {
        if (gameObject.activeSelf == true)
            StartCoroutine(DisableTimer(5f));
    }

}


