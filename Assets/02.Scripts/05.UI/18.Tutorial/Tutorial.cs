using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject tutText;
    [SerializeField] private GameObject tutMask;
    [SerializeField] private SkipPanel skipPanel;
    [SerializeField] private Button skip;

    private List<TutorialDB> tutIndex;

    private WaitUntil click;
    private bool isConfirm;

    private Vector2 tempVector;

    private void Awake()
    {
        DialogManager.Instance.Tutorial = this;

        click = Click();
        tempVector = new Vector2();
        tutIndex = DataManager.Instance.TutorialDB.ItemsList;

        skip.onClick.AddListener(() =>
        {
            skipPanel.gameObject.SetActive(true);
        });
        
        skipPanel.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private IEnumerator CoTutorial()
    {
        Time.timeScale = 0;
        foreach (TutorialDB data in tutIndex)
        {
            tutMask.GetComponentInChildren<CutOutMask>().maskable = false;
            tutMask.GetComponentInChildren<CutOutMask>().maskable = true;
            SetMask(data.Xpos, data.Ypos, data.Width, data.Height);
            SetText(data.TextXpos, data.TextYpos, data.TextWidth, data.TextHeight, data.Text);
            if (skipPanel.isDone == false)
            {
                isConfirm = false;
                yield return click;
            }
            else isConfirm = true;
            if(Time.timeScale > 0) Time.timeScale = 0;
        }
        Time.timeScale = 1;
        skipPanel.isDone = true;
        this.gameObject.SetActive(false);
        yield return null;
    }

    private WaitUntil Click()
    {
        click = new WaitUntil(() =>
        {
            if (skipPanel.isDone == true) return true;
            return isConfirm;
        });

        return click;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isConfirm = true;
    }

    private void SetMask(float x, float y, float w, float h)
    {
        tutMask.transform.localPosition = SetVector(x, y);
        tutMask.GetComponent<RectTransform>().sizeDelta = SetVector(w, h);
    }

    private void SetText(float x, float y, float w, float h, string text)
    {
        tutText.transform.localPosition = SetVector(x, y);
        tutText.GetComponent<RectTransform>().sizeDelta = SetVector(w, h);
        tutText.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    private Vector2 SetVector(float x, float y)
    {
        tempVector.x = x;
        tempVector.y = y;

        return tempVector;
    }

    public void CoStart()
    {
        this.gameObject.SetActive(true);
        GameManager.Instance.joyStick.AutoFalse();
        StartCoroutine(CoTutorial());
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}