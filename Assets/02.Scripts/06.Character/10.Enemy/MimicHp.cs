using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MimicHp : MonoBehaviour
{
    public MimicEnemy mimic;
    public Slider hpSlider;
    public float gameDuration;

    private void OnEnable()
    {
        gameDuration = 30f;
        StartCoroutine(DecreaseHPOverTime());
    }

    private IEnumerator DecreaseHPOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < gameDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / gameDuration;
            hpSlider.value = 1f - normalizedTime;

            yield return null;
        }
        GameManager.Instance.GoldDungeonEndilg();
        mimic.gameObject.SetActive(false);
    }
}
