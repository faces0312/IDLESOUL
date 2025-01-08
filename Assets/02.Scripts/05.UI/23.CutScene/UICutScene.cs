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

    private Dictionary<string, Sprite> dicImages = new Dictionary<string, Sprite>();

    private void Awake()
    {
        cutSceneImg = GetComponent<Image>();

        startColor = new Color(170f / 255f, 170f / 255f, 170f / 255f, 1f);
        endColor = cutSceneImg.color;

        soulImg.color = new Color(soulImg.color.r, soulImg.color.g, soulImg.color.b, 0f);

        originBackPos = backGroundImg.transform.localPosition;
        originSoulPos = soulImg.transform.localPosition;

        var image1 = Resources.Load<Sprite>("Sprite/TalkSprite/Carmilla");
        var image2 = Resources.Load<Sprite>("Sprite/TalkSprite/Claris");
        var image3 = Resources.Load<Sprite>("Sprite/TalkSprite/Fleur");
        var image4 = Resources.Load<Sprite>("Sprite/TalkSprite/Luen");
        dicImages.Add("카르밀라", image1);
        dicImages.Add("클라리스", image2);
        dicImages.Add("플뢰르", image3);
        dicImages.Add("루엔", image4);

        GameManager.Instance.OnGameClearEvent += ResetValue;
        GameManager.Instance.OnGameOverEvent += ResetValue;
    }

    private void OnEnable()
    {
        // TODO : 컷신 때 게임이 멈춰야함
        //Time.timeScale = 0f;

        // 활성화 시
        var seq = DOTween.Sequence();

        seq.Append(cutSceneImg.DOColor(startColor, 0.1f));//.OnComplete(() => soulImg.gameObject.SetActive(true));
        seq.Append(soulImg.DOFade(1f, 0.1f));
        seq.Append(cutSceneImg.DOColor(endColor, 0.3f)).OnComplete(() => soulImg.gameObject.SetActive(true));
        seq.Play().SetUpdate(true);

        soulImg.transform.DOLocalMoveY(70, 0.5f);

        Invoke("TweenImage", 1f);
    }

    private void OnDisable()
    {
        ResetValue();
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
        seq2.Play().SetUpdate(true).OnComplete(() =>
        {
            gameObject.SetActive(false);
            //Time.timeScale = 1f;
        });
    }

    private void ResetValue()
    {
        cutSceneImg.color = endColor;
        backGroundImg.transform.localPosition = originBackPos;
        soulImg.transform.localPosition = originSoulPos;
        soulImg.color = new Color(soulImg.color.r, soulImg.color.g, soulImg.color.b, 0f);
    }

    public void SetSoulSprite(string name)
    {
        if(dicImages.ContainsKey(name))
        {
            soulImg.sprite = dicImages[name];
        }
    }
}
