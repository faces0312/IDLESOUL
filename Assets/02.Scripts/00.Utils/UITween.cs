using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public static class UITween
{
    public static void ShowUI(GameObject go)
    {
        go.SetActive(true);

        var seq = DOTween.Sequence();

        seq.Append(go.transform.DOScale(1.1f, 0.2f));
        seq.Append(go.transform.DOScale(1f, 0.1f));

        seq.Play().SetUpdate(true);
    }

    public static void HideUI(GameObject go)
    {
        var seq = DOTween.Sequence();

        seq.Append(go.transform.DOScale(1.1f, 0.1f));
        seq.Append(go.transform.DOScale(0.05f, 0.2f));

        seq.Play().SetUpdate(true).OnComplete(() =>
        {
            go.SetActive(false);
        });
    }

    public static void OnClickEffect(GameObject button)
    {
        var seq = DOTween.Sequence();

        seq.Append(button.transform.DOScale(0.95f, 0.1f));
        seq.Append(button.transform.DOScale(1.05f, 0.1f));
        seq.Append(button.transform.DOScale(1f, 0.1f));
        seq.Play().SetUpdate(true);
    }
}
