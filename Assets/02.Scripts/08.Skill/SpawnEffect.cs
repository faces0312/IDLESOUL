using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    private Transform playerTransform;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        playerTransform = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;

        if (Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
