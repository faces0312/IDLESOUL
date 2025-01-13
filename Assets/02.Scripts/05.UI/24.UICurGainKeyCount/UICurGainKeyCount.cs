using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UICurGainKeyCount : UIB
{
    [SerializeField] private TextMeshProUGUI curGainKeyCount;
    private GameManager gameManager; //참조를 미리 해놓고 카운팅에 업데이트하여 최적화를 생각함

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void PrintGainKeyCount(int gainKeyCount)
    {
        curGainKeyCount.text = ;
    }
}
