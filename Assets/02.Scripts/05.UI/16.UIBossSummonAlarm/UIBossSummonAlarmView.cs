using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBossSummonAlarmView : MonoBehaviour, IUIBase
{
    [SerializeField] private Image backGroundImage;
    [SerializeField] private TextMeshProUGUI BossSummonAlarmTmp;
    [SerializeField] private GameObject BossNameFrame;
    [SerializeField] private TextMeshProUGUI BossNameLabel;

    public string BossSummonAlarmText;

    public void HideUI()
    {
        BossNameLabel.gameObject.SetActive(false);
        BossNameFrame.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        BossNameLabel.gameObject.SetActive(false);
        BossNameFrame.SetActive(false);
        backGroundImage.material.SetFloat("_FadeAmount", 1.0f);
    }

    public void ShowUI()
    {
        backGroundImage.material.SetFloat("_FadeAmount", 1.0f);
        gameObject.SetActive(true);
        StartCoroutine(TextPrint());
        StartCoroutine(BossAlarmFadeIn());
    }

    public void UpdateUI()
    {

    }
    public void BossNameSet(string bossName)
    {
        BossNameLabel.text = bossName;
    }

    IEnumerator TextPrint()
    {
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.Clear();

        for (int i = 0; i < BossSummonAlarmText.Length; i++)
        {
            strBuilder.Append(BossSummonAlarmText[i]);
            BossSummonAlarmTmp.text = strBuilder.ToString();

            yield return new WaitForSeconds(0.1f);
        }

        BossNameFrame.SetActive(true);
        BossNameLabel.gameObject.SetActive(true);
    }

    IEnumerator BossAlarmFadeIn()
    {
        float time = backGroundImage.material.GetFloat("_FadeAmount");
        while (time >= -1)
        {
            time -= Time.deltaTime;
            backGroundImage.material.SetFloat("_FadeAmount", time);

            yield return null;
        }

    }
}
