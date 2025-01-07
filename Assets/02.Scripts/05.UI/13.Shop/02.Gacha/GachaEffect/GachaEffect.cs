using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GachaEffect : MonoBehaviour
{
    [SerializeField] private Image gachaImage;
    [SerializeField] private TextMeshProUGUI gachaText;


    private void Start()
    {
        EventManager.Instance.OnPickUpSoul += ShowEffect;
        this.gameObject.SetActive(false);
        DOTween.Init();
        DOTween.useSafeMode = true;
    }

    private void MakeSquence()
    {
        Time.timeScale = 0;
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(gachaImage.transform.DOMoveY(600, 0));
        seq.Append(gachaText.DOFade(0, 0));
        seq.Append(gachaText.DOFade(1, 1));
        seq.Append(gachaText.DOFade(0, 0.5f));
        seq.Append(gachaImage.DOColor(Color.white, 1));
        seq.Append(gachaImage.transform.DOMoveY(-600, 1));
        seq.Append(gachaImage.DOColor(Color.black, 1));
        seq.OnComplete(() =>
        {
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        });
    }

    public void ShowEffect(SoulDB soul)
    {
        this.gameObject.SetActive(true);
        gachaImage.sprite = Resources.Load<Sprite>(soul.SpritePath);
        gachaImage.color = Color.black;
        gachaText.text = soul.Descripton;
        MakeSquence();
    }
}