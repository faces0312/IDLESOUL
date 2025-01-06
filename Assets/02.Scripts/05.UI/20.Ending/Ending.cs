using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] TextMeshProUGUI descSub;

    private void Awake()
    {
        desc.alpha = 0f;
        descSub.alpha = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowText", 1f);
    }

    private void ShowText()
    {
        var seq = DOTween.Sequence();

        seq.Append(desc.DOFade(1.0f, 1.0f));
        seq.Append(descSub.DOFade(1.0f, 1.0f));

        seq.Play().SetUpdate(true);
    }
}
