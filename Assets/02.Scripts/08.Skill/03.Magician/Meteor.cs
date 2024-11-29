using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float value;
    private float range;

    private float gravity = 9.8f;
    private float velocity;
    private float maxVelocity = 100f;

    private LayerMask layerMask;

    private Vector2 tempDir;
    private Vector3 screenCenterWorld;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        transform.localScale = new Vector3(range, range, range);

        screenCenterWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        screenCenterWorld.z = 0;

        tempDir = new Vector2(-1, -1);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : �缱���� �������� ���׿� ����
        // ��ũ�� �߾ӿ� �����ϸ� �ı�

        velocity += gravity * Time.deltaTime;
        velocity = Mathf.Min(velocity, maxVelocity);

        transform.Translate(tempDir * velocity * Time.deltaTime);

        if(transform.position.y < screenCenterWorld.y)
            Destroy(gameObject);
    }

    public void InitSettings(float value, float range)
    {
        this.value = value;
        this.range = range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject.layer, layerMask))
        {
            GameManager.Instance.enemies.Remove(collision.gameObject);  // �ӽ÷� ����
            Destroy(collision.gameObject);
            Debug.LogAssertion("Enemy Destroy");
        }
    }
}
