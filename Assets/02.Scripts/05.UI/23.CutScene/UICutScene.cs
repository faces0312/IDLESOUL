using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICutScene : MonoBehaviour
{
    [SerializeField] Image backGroundImg;
    [SerializeField] Image soulImg;
    private Image cutSceneImg;

    private Color startColor;
    private Color endColor;

    private Vector2 originBackPos;
    private Vector2 originSoulPos;

    private Dictionary<string, Image> dicImages = new Dictionary<string, Image>();

    private void Awake()
    {
        cutSceneImg = GetComponent<Image>();

        startColor = new Color(170f / 255f, 170f / 255f, 170f / 255f, 1f);
        endColor = cutSceneImg.color;

        soulImg.color = new Color(soulImg.color.r, soulImg.color.g, soulImg.color.b, 0f);

        originBackPos = backGroundImg.transform.localPosition;
        originSoulPos = soulImg.transform.localPosition;

        //Image[] images = Resources.LoadAll<Image>("Sprite/TalkSprite");

    }

    private void OnEnable()
    {
        // 활성화 시
        var seq = DOTween.Sequence();

        seq.Append(cutSceneImg.DOColor(startColor, 0.1f));//.OnComplete(() => soulImg.gameObject.SetActive(true));
        seq.Append(soulImg.DOFade(1f, 0.1f));
        seq.Append(cutSceneImg.DOColor(endColor, 0.3f)).OnComplete(() => soulImg.gameObject.SetActive(true));
        seq.Play().SetUpdate(true);

        soulImg.transform.DOLocalMoveY(70, 0.5f);

        Invoke("TweenImage", 1.5f);
    }

    private void OnDisable()
    {
        cutSceneImg.color = endColor;
        backGroundImg.transform.localPosition = originBackPos;
        soulImg.transform.localPosition = originSoulPos;
        soulImg.color = new Color(soulImg.color.r, soulImg.color.g, soulImg.color.b, 0f);
    }

    private void TweenImage()
    {
        // 활성화 이후 단계
        var seq = DOTween.Sequence();

        seq.Append(soulImg.transform.DOLocalMoveY(-300, 0.2f)).SetEase(Ease.InCubic);
        seq.Append(soulImg.DOFade(0f, 0.1f));
        seq.Play().SetUpdate(true);

        var seq2 = DOTween.Sequence();

        seq2.Append(backGroundImg.transform.DOLocalMoveX(-190, 0.3f)).SetEase(Ease.InCubic);
        seq2.Append(cutSceneImg.DOFade(0f, 0.2f));
        seq2.Play().SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
    }

    public void SetSoulSprite(Sprite spr)
    {
        soulImg.sprite = spr;
    }
}
