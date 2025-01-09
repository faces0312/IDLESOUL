using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] public SkillType skillType;

    [Header("Image")]
    [SerializeField] private Image skillImg;
    [SerializeField] private Image cooldownImg;

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject textBackground;

    [Header("CutScene")]
    [SerializeField] private GameObject cutSceneObj;

    private Button button;

    private float[] fillAmounts = new float[Const.MAX_SOUL];
    private float[] coolTimes = new float[Const.MAX_SOUL];
    private float[] curTimes = new float[Const.MAX_SOUL];
    public bool[] isUses = new bool[Const.MAX_SOUL];

    public int CurSoulIndex { get; set; }

    private void Awake()
    {
        button = GetComponent<Button>();
        cooldownImg.fillAmount = 0f;
        timeText.text = string.Empty;
        textBackground.SetActive(false);

        GameManager.Instance.skillButton = this;
    }

    void Start()
    {
        StartCoroutine(DelayedInitialization());
    }

    IEnumerator DelayedInitialization()
    {
        yield return null;

        button.onClick.AddListener(OnClickSkillButton);

        switch (skillType)
        {
            case SkillType.Default:
                GameManager.Instance.player.PlayerSouls.OnUpdateDefaultSprite += UpdateSkillImage;
                GameManager.Instance.playerController.OnSkill1 += OnClickSkillButton;
                break;
            case SkillType.Ultimate:
                GameManager.Instance.player.PlayerSouls.OnUpdateUltimateSprite += UpdateSkillImage;
                GameManager.Instance.playerController.OnSkill2 += OnClickSkillButton;
                break;
        }

        // TODO : 추후 제거
        GameManager.Instance.player.PlayerSouls.UpdateSkillSprite();
        GameManager.Instance.OnGameClearEvent += ResetAll;
        GameManager.Instance.OnGameOverEvent += ResetAll;

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

    public void OnClickSkillButton()
    {
        CurSoulIndex = GameManager.Instance.player.PlayerSouls.SpawnIndex;

        if (isUses[CurSoulIndex]) return;
        isUses[CurSoulIndex] = true;
        textBackground.SetActive(isUses[CurSoulIndex]);

        Soul soul = GameManager.Instance.player.PlayerSouls.CurrentSoul;

        if (cutSceneObj != null)
        {
            cutSceneObj.SetActive(true);
            ShowCutScene(soul, soul.Skills[(int)skillType]);
        }

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

            yield return null;
        }

        isUses[soulIndex] = false;
    }

    private void UpdateSkillImage(Sprite sprite)
    {
        skillImg.sprite = sprite;
    }

    private void ResetAll()
    {
        for (int i = 0; i < Const.MAX_SOUL; ++i)
        {
            fillAmounts[i] = 0f;
            coolTimes[i] = 0f;
            curTimes[i] = 0f;
            isUses[i] = false;
        }

        timeText.text = string.Empty;
        textBackground.SetActive(false);
        cooldownImg.fillAmount = 0f;
    }

    private void ShowCutScene(Soul soul, Skill skill)
    {
        if(cutSceneObj.TryGetComponent(out UICutScene cutScene))
        {
            cutScene.SetSoulSprite(soul.soulName, skill.skillName);
        }
    }
}
