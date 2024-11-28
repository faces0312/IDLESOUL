using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageSelector : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject StagePanel;
    [SerializeField] private Transform content;

    private readonly string POOL_DICT_KEY = "StageSelector";
    private readonly string POOL_KEY = "StageSelectors";

    private ObjectPool pool;
    private Stack<Vector2> stagePositions;

    private void OnEnable()
    {
        stagePositions = new Stack<Vector2>();
        if (ObjectPoolManager.Instance.GetPool(POOL_DICT_KEY, POOL_KEY) == null)
        {
            pool = new ObjectPool(POOL_KEY, 5, "Prefabs/Sample/Stage", content);
            ObjectPoolManager.Instance.AddPool(POOL_DICT_KEY, pool);
        }
        else pool = ObjectPoolManager.Instance.GetPool(POOL_DICT_KEY, POOL_KEY);

        pool.SetActiveAllTrue();

        for (int i = 0; i < pool.GetPool().Count; i++)
        {
            stagePositions.Push(pool.GetObject().transform.position);
        }
    }

    private void Update()
    {
        Debug.Log(content.position);
    }

    private void OnDisable()
    {
        pool.SetActiveAllFalse();
    }

    private void InfiniteScroll(Vector2 position)
    {
        float end = 0f;
        if(position.x < end)
        {

        }
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
