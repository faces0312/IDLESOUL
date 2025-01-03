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
    private List<Vector2> zoomPosition;


    private void Start()
    {
        EventManager.Instance.OnPickUpSoul += ShowEffect;
        this.gameObject.SetActive(false);
    }

    private void MakeSquence()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(gachaImage.transform.DOMoveY(600, 0));
        seq.Append(gachaText.DOFade(0, 0));
        seq.Append(gachaText.DOFade(1, 3));
        seq.Append(gachaText.DOFade(0, 1));
        seq.Append(gachaImage.DOColor(Color.white, 2));
        seq.Append(gachaImage.transform.DOMoveY(-600, 2));
        seq.Append(gachaImage.DOColor(Color.black, 2));
        seq.OnComplete(() =>
        {
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