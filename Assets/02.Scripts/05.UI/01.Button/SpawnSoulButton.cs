using System.Collections;
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

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject textBackground;

    private Button button;
    private float coolTime = 2f;
    private float curTime;
    private bool isSpawn;

    private SpawnCoolTime spawnCoolTime;

    private void Awake()
    {
        button = GetComponent<Button>();
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
    }

    private void OnClickSpawnSoulButton()
    {
        if (spawnCoolTime.IsSpawn) return;
        if (GameManager.Instance.player.PlayerSouls.SpawnIndex == index) return;
        spawnCoolTime.IsSpawn = true;
        textBackground.SetActive(spawnCoolTime.IsSpawn);

        GameManager.Instance.player.PlayerSouls.SpawnSoul(index);

        spawnCoolTime.StartCoolTime();
    }

    private void CalcCoolTime()
    {
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
        textBackground.SetActive(isSpawn);
    }
}
