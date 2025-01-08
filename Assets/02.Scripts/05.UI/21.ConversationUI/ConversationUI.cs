using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterText;
    [SerializeField] private Button skip;

    public Action CycleDone;
    public int CurCycle = 0;

    private Coroutine conversation;
    private List<Dialog> Dialog;
    private WaitUntil click;
    private bool isConfirm;
    private bool isSkip;
    private string Name;

    private void Awake()
    {
        DialogManager.Instance.ConversationUI = this;

        Dialog = new List<Dialog>();
        click = new WaitUntil(() => //Ŭ�� �� true�� ��ȯ
        {
            if (isSkip == true)
            {
                return true;
            }
            else return isConfirm;
        });
        if (GameManager.Instance.player.UserData == null) Name = "a";
        else Name = GameManager.Instance.player.UserData.NickName;

        this.gameObject.SetActive(false);
    }

    private void Print(Dialog log) //���
    {
        this.characterImage.sprite = Resources.Load<Sprite>(log.CharacterImage);
        this.characterImage.color = HexaToColor(log.ImageColor);
        this.characterName.text = ReplaceHolder(log.name);
        this.characterText.text = ReplaceHolder(log.text);
    }

    private string ReplaceHolder(string text)
    {
        text.Replace("{Name}", Name);
        return text;
    }

    private Color HexaToColor(string hexa) //����ڵ� ��� �÷��� ����
    {
        if (hexa != null)
        {
            ColorUtility.TryParseHtmlString(hexa, out Color color);
            return color;
        }
        else return Color.white;
    }

    public void StartConversation(int cycle) //�ܺο��� ���� ������ �ڷ�ƾ ���� Ʈ����
    {
        this.gameObject.SetActive(true);
        conversation = StartCoroutine(CoConversation(cycle));
        CurCycle = cycle;
    }

    private IEnumerator CoConversation(int cycle) //����Ŭ�� �Է��ϸ� �ش� ����Ŭ�� ��� ������ ���
    {
        while (true)
        {
            this.gameObject.SetActive(true);
            Dialog = DataManager.Instance.Dialog.GetByCycle(cycle);
            for (int i = 1; i < Dialog.Count; i++)
            {
                if (Dialog[i].ConversationType == 1 || Dialog[i].ConversationType == 2)
                {
                    Print(Dialog[i]);
                }
                else
                {
                    i = Selection(Dialog[i + 1], Dialog[i + 2]);
                }

                if (Dialog[i].NextIndex != 0) i = Dialog[i].NextIndex - 1;

                isConfirm = false;
                yield return click;
            }
            StopCoroutine(conversation);
            this.gameObject.SetActive(false);
            CycleDone?.Invoke();
            yield return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isConfirm = true;
    }

    public int Selection(Dialog log1, Dialog log2)
    {
        //������ 1�� = log1
        //������ 2�� = log2
        return 0; //������ ����� ���� ���� index�� ��ȯ
    }
}
