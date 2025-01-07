﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSoulButton : MonoBehaviour
{
    [Header("SoulIndex")]
    [SerializeField] private int index;

    [Header("Image")]
    [SerializeField] private Image soulImg;
    [SerializeField] private Image cooldownImg;
    [SerializeField] private Image frameImg;

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject textBackground;

    private Button button;
    private float coolTime = 2f;
    private float curTime;
    private bool isSpawn;

    private SpawnCoolTime spawnCoolTime;

    private Color iconAlpha;
    private Color iconNonAlpha;

    private void Awake()
    {
        button = GetComponent<Button>();
        iconAlpha = soulImg.color;
        iconNonAlpha = new Color(iconAlpha.r, iconAlpha.g, iconAlpha.b, 0f);
        GameManager.Instance.spawnSoul = this;
    }

    void Start()
    {
        button.onClick.AddListener(OnClickSpawnSoulButton);
        
        if(transform.parent.gameObject.TryGetComponent(out spawnCoolTime))
        {
            spawnCoolTime.OnUpdateCoolTime += CalcCoolTime;
        }

        spawnCoolTime.IsSpawn = true;
        StartCoroutine(CoroutineCoolTime());
        GameManager.Instance.playerController.OnSwitch += HandleSwitch;

        GameManager.Instance.player.PlayerSouls.OnUpdateSoulIcon += UpdateIcon;
        GameManager.Instance.player.PlayerSouls.OnUpdateSpawnSoul += UpdateSpawn;
        UpdateIcon();
        GameManager.Instance.OnGameClearEvent += ResetAll;
        GameManager.Instance.OnGameOverEvent += ResetAll;
    }

    private void HandleSwitch(int switchIndex)
    {
        if (switchIndex == index + 1) // index가 0부터 시작한다고 가정
        {
            OnClickSpawnSoulButton();
        }
    }

    private void OnClickSpawnSoulButton()
    {
        if (spawnCoolTime.IsSpawn) return;
        if (GameManager.Instance.player.PlayerSouls.SpawnIndex == index) return;
        if (GameManager.Instance.player.PlayerSouls.SoulSlot[index] == null) return; //해당 버튼에 Soul이 등록되지 않은 경우
        
        spawnCoolTime.IsSpawn = true;

        GameManager.Instance.player.PlayerSouls.SpawnSoul(index);

        spawnCoolTime.StartCoolTime();
    }

    private void CalcCoolTime()
    {
        textBackground.SetActive(true);
        StartCoroutine(CoroutineCoolTime());
    }

    private IEnumerator CoroutineCoolTime()
    {
        float startTime = Time.time;
        float fillAmount = 1f;

        while (fillAmount > 0f)
        {
            curTime = Time.time - startTime;

            fillAmount = 1f - Utils.Percent(curTime, coolTime);
            cooldownImg.fillAmount = fillAmount;
            timeText.text = $"{coolTime - curTime:F1}";
            yield return null;
        }

        fillAmount = 0f;
        timeText.text = string.Empty;
        spawnCoolTime.IsSpawn = false;
        textBackground.SetActive(false);
    }

    private void ResetAll()
    {
        curTime = 0f;
        cooldownImg.fillAmount = 0f;
        timeText.text = string.Empty;
        spawnCoolTime.IsSpawn = false;
        textBackground.SetActive(false);
        frameImg.color = Color.white;
    }

    private void UpdateSpawn()
    {
        if(GameManager.Instance.player.PlayerSouls.SpawnIndex == index)
            frameImg.color = Color.red;
        else
            frameImg.color = Color.white;
    }

    public void UpdateIcon()
    {
        // TODO : 소울 아이콘 변경
        if (GameManager.Instance.player.PlayerSouls.SoulSlot[index] != null)
        {
            soulImg.sprite = GameManager.Instance.player.PlayerSouls.SoulSlot[index].icon;
            soulImg.color = iconAlpha;
        }
        else
        {
            soulImg.color = iconNonAlpha;
        }
    }
}
