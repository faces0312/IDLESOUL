using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public Vector3 dir;
    public LayerMask TargetLayer;

    public void Movedir(Vector3 dir)
    {
        this.dir = dir;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * MoveSpeed * dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {
            Debug.Log($"플레이어의 공격이 {other.name}에 충돌");
            ObjectPoolManager.Instance.GetPool("playerProjectile", Utils.POOL_KEY_PLAYERPROJECTILE).GetObject();
            gameObject.SetActive(false);
        }
    }
}
