using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBackGroundAnimation : MonoBehaviour
{
    [SerializeField] private Image TitleImg;

    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(StartSceneEffect());
    }

    IEnumerator StartSceneEffect()
    {
        TitleImg.material.SetFloat("_FadeAmount", 1.0f);

        float time = TitleImg.material.GetFloat("_FadeAmount");
        while (time >= -1)
        {
            time -= Time.deltaTime * 2.0f;
            TitleImg.material.SetFloat("_FadeAmount", time);

            yield return new WaitForSeconds(0.01f);
        }

    }
}
