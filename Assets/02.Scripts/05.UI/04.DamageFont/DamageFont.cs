using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageFont : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageFont;

    private float moveSpeed = 1.5f;
    private float fadeSpeed = 0.5f;

    private BigInteger damage;
    private Camera mainCam;

    private Vector3 originScale;

    private float fixedSizeOnScreen = 100f;

    private void Awake()
    {
        mainCam = Camera.main;
        originScale = damageFont.transform.localScale;
    }

    private void Update()
    {
        FixedScreenSize();
    }

    private void OnEnable()
    {
        StartCoroutine(CoroutineDamageView());
    }

    private void OnDisable()
    {
        damageFont.transform.localScale = originScale;
    }

    private IEnumerator CoroutineDamageView()
    {
        Vector3 startPosition = transform.position;
        Color startColor = damageFont.color;

        while (damageFont.color.a > 0)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            startColor.a -= fadeSpeed * Time.deltaTime;
            damageFont.color = startColor;

            yield return null;
        }
    }

    private void FixedScreenSize()
    {
        //  카메라와의 거리 계산
        float distance = Vector3.Distance(damageFont.transform.position, mainCam.transform.position);

        //  카메라의 Field of View (FOV)와 화면 크기를 고려한 스케일 보정
        float fov = mainCam.fieldOfView;
        float screenHeight = Screen.height;

        //  스케일 보정 공식: 카메라의 거리와 시야각을 고려해 크기를 고정
        float scaleFactor = 2f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad) / screenHeight;

        //  스케일을 고정 크기에 맞게 조정
        float sizeOnScreen = fixedSizeOnScreen * scaleFactor;
        damageFont.transform.localScale = Vector3.one * sizeOnScreen;
    }

    public void SetDamage(BigInteger damage)
    {
        this.damage = damage;
        damageFont.text = Utils.FormatBigInteger(this.damage);
    }
}
