using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICutScene : MonoBehaviour
{
    [SerializeField] Image backGroundImg;
    [SerializeField] Image soulImg;
    [SerializeField] TextMeshProUGUI skillNameText;
    private Image cutSceneImg;

    private Color startColor;
    private Color endColor;

    private Vector2 originBackPos;
    private Vector2 originSoulPos;
    private Vector2 originTextPos;

    private Dictionary<string, Sprite> dicImages = new Dictionary<string, Sprite>();

    private void Awake()
    {
        cutSceneImg = GetComponent<Image>();

        startColor = new Color(170f / 255f, 170f / 255f, 170f / 255f, 1f);
        endColor = cutSceneImg.color;

        soulImg.color = new Color(soulImg.color.r, soulImg.color.g, soulImg.color.b, 0f);

        originBackPos = backGroundImg.transform.localPosition;
        originSoulPos = soulImg.transform.localPosition;
        originTextPos = skillNameText.transform.localPosition;

        var image1 = Resources.Load<Sprite>("Sprite/TalkSprite/Carmilla");
        var image2 = Resources.Load<Sprite>("Sprite/TalkSprite/Claris");
        var image3 = Resources.Load<Sprite>("Sprite/TalkSprite/Fleur");
        var image4 = Resources.Load<Sprite>("Sprite/TalkSprite/Luen");
        dicImages.Add("ī���ж�", image1);
        dicImages.Add("Ŭ�󸮽�", image2);
        dicImages.Add("�÷ڸ�", image3);
        dicImages.Add("�翣", image4);

        GameManager.Instance.OnGameClearEvent += ResetValue;
        GameManager.Instance.OnGameOverEvent += ResetValue;
    }

    private void OnEnable()
    {
        // ��ų �ƽ� ����
        Time.timeScale = 0f;
        GameManager.Instance.isCutScene = true;

        // Ȱ��ȭ ��
        var seq = DOTween.Sequence();

        seq.Append(cutSceneImg.DOColor(startColor, 0.1f));
        seq.Append(soulImg.DOFade(1f, 0.1f));
        seq.Append(cutSceneImg.DOColor(endColor, 0.3f)).OnComplete(() => soulImg.gameObject.SetActive(true));
        seq.Play().SetUpdate(true);

        var seq2 = DOTween.Sequence();
        seq2.Append(soulImg.transform.DOLocalMoveY(70, 0.5f));
        seq2.Play().SetUpdate(true);

        var seq3 = DOTween.Sequence();
        seq3.Append(skillNameText.transform.DOLocalMoveY(455, 0.5f));
        seq3.Play().SetUpdate(true);

        //Invoke("TweenImage", 1f);
        StartCoroutine(CoroutineTween(1f));
    }

    private void OnDisable()
    {
        ResetValue();
    }

    private void TweenImage()
    {
        // Ȱ��ȭ ���� �ܰ�
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

        var seq3 = DOTween.Sequence();
        seq3.Append(skillNameText.transform.DOLocalMoveY(-535, 0.2f).SetEase(Ease.InCubic));
        seq3.Append(skillNameText.DOFade(0f, 0.1f));
        seq3.Play().SetUpdate(true);
    }

    private IEnumerator CoroutineTween(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        // Ȱ��ȭ ���� �ܰ�
        var seq = DOTween.Sequence();

        seq.Append(soulImg.transform.DOLocalMoveY(-300, 0.2f)).SetEase(Ease.InCubic);
        seq.Append(soulImg.DOFade(0f, 0.1f));
        seq.Play().SetUpdate(true);

        var seq2 = DOTween.Sequence();

        seq2.Append(backGroundImg.transform.DOLocalMoveX(-190, 0.3f)).SetEase(Ease.InCubic);
        seq2.Append(cutSceneImg.DOFade(0f, 0.2f));
        seq2.Play().SetUpdate(true).OnComplete(() =>
        {
            // ��ų �ƽ� ����
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.isCutScene = false;
        });

        var seq3 = DOTween.Sequence();
        seq3.Append(skillNameText.transform.DOLocalMoveY(-535, 0.2f).SetEase(Ease.InCubic));
        seq3.Append(skillNameText.DOFade(0f, 0.1f));
        seq3.Play().SetUpdate(true);
    }

    private void ResetValue()
    {
        cutSceneImg.color = endColor;
        backGroundImg.transform.localPosition = originBackPos;
        soulImg.transform.localPosition = originSoulPos;
        skillNameText.transform.localPosition = originTextPos;
        soulImg.color = new Color(soulImg.color.r, soulImg.color.g, soulImg.color.b, 0f);
        skillNameText.color = new Color(skillNameText.color.r, skillNameText.color.g, skillNameText.color.b, 1f);
    }

    public void SetSoulSprite(string soulName, string skillName)
    {
        if(dicImages.ContainsKey(soulName))
        {
            soulImg.sprite = dicImages[soulName];
            skillNameText.text = skillName;

            switch(soulName)
            {
                case "Ŭ�󸮽�":
                    skillNameText.color = Color.red;
                    break;
                case "�÷ڸ�":
                    skillNameText.color = Color.blue;
                    break;
                case "�翣":
                    skillNameText.color = Color.green;
                    break;
                case "ī���ж�":
                    skillNameText.color = Color.red;
                    break;
            }
        }
    }
}
