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

    private Coroutine conversation;
    private List<Dialogue> dialogue;
    private WaitUntil click;
    private bool isConfirm;
    private bool isSkip;
    private string Name;

    private void Start()
    {
        dialogue = new List<Dialogue>();
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
    }

    private void Print(Dialogue log) //���
    {
        this.characterImage.sprite = Resources.Load<Sprite>(log.CharacterImage);
        this.characterImage.color = HexaToColor(log.ImageColor);
        this.characterName.text = ReplaceHolder(log.name);
        this.characterText.text = ReplaceHolder(log.text);
    }

    private string ReplaceHolder(string text)
    {
        text.Replace("${Name}", Name);
        return text;
    }

    private Color HexaToColor(string hexa) //����ڵ� ��� �÷��� ����
    {
        if(hexa != null)
        {
            ColorUtility.TryParseHtmlString(hexa, out Color color);
            return color;
        }
        else return Color.white;
    }

    public void StartConversation(int cycle) //�ܺο��� ���� ������ �ڷ�ƾ ���� Ʈ����
    {
        conversation = StartCoroutine(CoConversation(cycle));
    }

    private IEnumerator CoConversation(int cycle) //����Ŭ�� �Է��ϸ� �ش� ����Ŭ�� ��� ������ ���
    {
        while(true)
        {
            this.gameObject.SetActive(true);
            dialogue = DataManager.Instance.Dialogue.GetByCycle(cycle);
            foreach(var log in dialogue)
            {
                Print(log);
                isConfirm = false;
                yield return click; //Ŭ�� �� ��� ����
            }
            StopCoroutine(conversation);
            this.gameObject.SetActive(false);
            yield return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isConfirm = true;
    }

    private void StartTutorial()
    {

    }
}
