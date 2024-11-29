using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject StagePanel;
    [SerializeField] private Transform content;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRect;

    private readonly string POOL_DICT_KEY = "StageSelector";
    private readonly int POOL_KEY = 0;

    private ObjectPool pool;
    private float srReach = 1f;

    private void OnEnable()
    {
        if (ObjectPoolManager.Instance.GetPool(POOL_DICT_KEY, POOL_KEY) == null)
        {
            pool = new ObjectPool(POOL_KEY, 10, "Prefabs/Sample/Stage", content);
            ObjectPoolManager.Instance.AddPool(POOL_DICT_KEY, pool);
        }
        else pool = ObjectPoolManager.Instance.GetPool(POOL_DICT_KEY, POOL_KEY);

        for (int i = 0; i < pool.GetPool().Count(); i++) 
        {
            pool.GetObject().GetComponent<Stage>().SetStageName(i.ToString());
        }

        scrollRect.onValueChanged.AddListener(InfiniteScroll);
        pool.SetActiveAllTrue();
    }

    private void OnDisable()
    {
        pool.SetActiveAllFalse();
        scrollRect.onValueChanged.RemoveListener(InfiniteScroll);
    }

    private void InfiniteScroll(Vector2 position)
    {
        // Todo : position.x 값이 -400미만이라면 가장 왼쪽의 오브젝트를 오른쪽에 재생성
        // 반대로 -400이상이라면 가장 오른쪽의 오브젝트를 왼쪽에 재생성
        // 가장 왼쪽의 오브젝트는 Queue순으로 Dequeue로 해결가능
        // 가장 오른쪽의 오브젝트는?

        
    }

    public void Initialize()
    {

    }

    public void ShowUI()
    {
        StagePanel.SetActive(true);
    }

    public void HideUI()
    {
        StagePanel.SetActive(false);
    }

    public void UpdateUI()
    {

    }
}
