using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UICurrentEnemyCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI curEnemyCount;
    private int lastEnemyCount = -1; //제일 마지막에 측정된 enemy의 갯수 
    private GameManager gameManager; //참조를 미리 해놓고 카운팅에 업데이트하여 최적화를 생각함

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void LateUpdate()
    {
        //Text 출력 빈도 줄이기 
        int currentEnemyCount = gameManager.enemies.Count;
        if (lastEnemyCount != currentEnemyCount)
        {
            curEnemyCount.text = currentEnemyCount.ToString();
            lastEnemyCount = currentEnemyCount;
        }
    }
}
