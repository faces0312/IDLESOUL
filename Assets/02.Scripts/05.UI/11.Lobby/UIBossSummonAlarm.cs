using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIBossSummonAlarm : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bossSummonAlarmText;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            bossSummonAlarmText.DOFade(0f, 1f)
                               .OnComplete(() => bossSummonAlarmText.DOFade(1f, 1f));
        }

    }

}
