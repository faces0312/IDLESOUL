using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    private Transform playerTransform;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        playerTransform = GameManager.Instance.player.transform;
    }

    void Update()
    {
        transform.position = playerTransform.position;

        if (Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
