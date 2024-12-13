using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    //private const int MAX_SOUL = 3;

    [Header("Type")]
    [SerializeField] private SkillType skillType;

    [Header("Image")]
    [SerializeField] private Image skillImg;
    [SerializeField] private Image cooldownImg;

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject textBackground;

    private Button button;
    private float coolTime;
    private float curTime;
    private bool isUse;

    private float[] fillAmounts = new float[Const.MAX_SOUL];
    private float[] coolTimes = new float[Const.MAX_SOUL];
    private float[] curTimes = new float[Const.MAX_SOUL];
    private bool[] isUses = new bool[Const.MAX_SOUL];

    public int CurSoulIndex { get; set; }

    private void Awake()
    {
        button = GetComponent<Button>();
        cooldownImg.fillAmount = 0f;
        timeText.text = string.Empty;
        textBackground.SetActive(false);
    }

    void Start()
    {
        button.onClick.AddListener(OnClickSkillButton);

        switch(skillType)
        {
            case SkillType.Default:
                GameManager.Instance.player.PlayerSouls.OnUpdateDefaultSprite += UpdateSkillImage;
                break;
            case SkillType.Ultimate:
                GameManager.Instance.player.PlayerSouls.OnUpdateUltimateSprite += UpdateSkillImage;
                break;
        }

        // TODO : ���� ����
        GameManager.Instance.player.PlayerSouls.UpdateSkillSprite();

        TestManager.Instance.OnClickRegisterSoul(); // TODO : �� ��ĥ�� ��ġ ����

        CurSoulIndex = GameManager.Instance.player.PlayerSouls.SpawnIndex;
    }

    private void Update()
    {
        if (isUses[CurSoulIndex])
        {
            fillAmounts[CurSoulIndex] = 1f - Utils.Percent(curTimes[CurSoulIndex], coolTimes[CurSoulIndex]);
            timeText.text = $"{coolTimes[CurSoulIndex] - curTimes[CurSoulIndex]:F1}";
        }
        else
        {
            fillAmounts[CurSoulIndex] = 0f;
            timeText.text = string.Empty;
            textBackground.SetActive(isUses[CurSoulIndex]);
        }

        cooldownImg.fillAmount = fillAmounts[CurSoulIndex];
    }

    private void OnClickSkillButton()
    {
        CurSoulIndex = GameManager.Instance.player.PlayerSouls.SpawnIndex;

        if (isUses[CurSoulIndex]) return;
        isUses[CurSoulIndex] = true;
        textBackground.SetActive(isUses[CurSoulIndex]);

        Soul soul = GameManager.Instance.player.PlayerSouls.CurrentSoul;
        soul.UseSkill(soul.Skills[(int)skillType]);

        coolTimes[CurSoulIndex] = soul.Skills[(int)skillType].CoolTime;
        StartCoroutine(CoroutineCoolTime());
    }

    private IEnumerator CoroutineCoolTime()
    {
        float startTime = Time.time;
        int soulIndex = CurSoulIndex;
        fillAmounts[soulIndex] = 1f;

        while (fillAmounts[soulIndex] > 0f)
        {
            curTimes[soulIndex] = Time.time - startTime;

            //fillAmount = 1f - Utils.Percent(curTimes[curSoulIndex], coolTimes[curSoulIndex]);
            //cooldownImg.fillAmount = fillAmount;
            //timeText.text = $"{coolTimes[curSoulIndex] - curTimes[curSoulIndex]:F1}";
            yield return null;
        }

        //fillAmount = 0f;
        //timeText.text = string.Empty;
        isUses[soulIndex] = false;
        //textBackground.SetActive(isUses[curSoulIndex]);
    }

    private void UpdateSkillImage(Sprite sprite)
    {
        skillImg.sprite = sprite;
    }
}
