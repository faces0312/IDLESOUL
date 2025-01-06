using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ScottGarland;

public class UIPlayerHPDisplayView : MonoBehaviour, IUIBase
{
    [SerializeField] private RectTransform HPFrontImg;
    [SerializeField] private TextMeshProUGUI HpText;

    public void HpRatioChange(BigInteger curHp , BigInteger maxHP)
    {
        ulong curHpNum = BigInteger.ToUInt64(curHp);
        ulong maxHpNum = BigInteger.ToUInt64(maxHP);
        float result = curHpNum / (float)maxHpNum;

        HPFrontImg.localScale = new Vector3(result, 1, 1);

        string curHealthString = Utils.FormatBigInteger(curHp);
        string maxHealthString = Utils.FormatBigInteger(maxHP);

        HpText.text = $"{curHealthString} / {maxHealthString}";
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void UpdateUI()
    {

    }
}
