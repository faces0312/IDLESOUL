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

        isSpawn = true;
        StartCoroutine(CoroutineCoolTime());
    }

    private void OnClickSpawnSoulButton()
    {
        if (isSpawn) return;
        if (GameManager.Instance.player.PlayerSouls.SpawnIndex == index) return;
        isSpawn = true;
        textBackground.SetActive(isSpawn);

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
        isSpawn = false;
        textBackground.SetActive(isSpawn);
    }
}
