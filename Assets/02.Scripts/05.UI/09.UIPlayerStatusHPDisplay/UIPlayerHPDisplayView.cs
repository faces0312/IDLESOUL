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
        long curHpNum = BigInteger.ToInt64(curHp);
        long maxHpNum = BigInteger.ToInt64(maxHP);

        // ulong을 double로 캐스팅하여 소수점 값을 계산
        double healthValue = (double)curHpNum / maxHpNum;

        HPFrontImg.localScale = new Vector3((float)healthValue, 1, 1);

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
