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
        click = new WaitUntil(() => //클릭 시 true를 반환
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

    private void Print(Dialogue log) //출력
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

    private Color HexaToColor(string hexa) //헥사코드 기반 컬러로 변경
    {
        if(hexa != null)
        {
            ColorUtility.TryParseHtmlString(hexa, out Color color);
            return color;
        }
        else return Color.white;
    }

    public void StartConversation(int cycle) //외부에서 접근 가능한 코루틴 시작 트리거
    {
        conversation = StartCoroutine(CoConversation(cycle));
    }

    private IEnumerator CoConversation(int cycle) //사이클을 입력하면 해당 사이클의 모든 내용을 출력
    {
        while(true)
        {
            this.gameObject.SetActive(true);
            dialogue = DataManager.Instance.Dialogue.GetByCycle(cycle);
            foreach(var log in dialogue)
            {
                Print(log);
                isConfirm = false;
                yield return click; //클릭 시 계속 진행
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
