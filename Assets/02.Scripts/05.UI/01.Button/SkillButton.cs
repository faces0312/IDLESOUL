using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
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

        // TODO : 추후 제거
        GameManager.Instance.player.PlayerSouls.UpdateSkillSprite();
    }

    private void OnClickSkillButton()
    {
        if (isUse) return;
        isUse = true;
        textBackground.SetActive(isUse);

        Soul soul = GameManager.Instance.player.PlayerSouls.CurrentSoul;
        soul.UseSkill(soul.Skills[(int)skillType]);

        coolTime = soul.Skills[(int)skillType].CoolTime;
        StartCoroutine(CoroutineCoolTime());
    }

    private IEnumerator CoroutineCoolTime()
    {
        float startTime = Time.time;
        float fiilAmount = 1f;

        while (fiilAmount > 0f)
        {
            curTime = Time.time - startTime;

            fiilAmount = 1f - Utils.Percent(curTime, coolTime);
            cooldownImg.fillAmount = fiilAmount;
            timeText.text = $"{coolTime - curTime:F1}";
            yield return null;
        }
        
        fiilAmount = 0f;
        timeText.text = string.Empty;
        isUse = false;
        textBackground.SetActive(isUse);
    }

    private void UpdateSkillImage(Sprite sprite)
    {
        skillImg.sprite = sprite;
    }
}
