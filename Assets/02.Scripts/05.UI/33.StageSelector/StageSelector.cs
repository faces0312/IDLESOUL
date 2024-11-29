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
        // Todo : position.x ���� -400�̸��̶�� ���� ������ ������Ʈ�� �����ʿ� �����
        // �ݴ�� -400�̻��̶�� ���� �������� ������Ʈ�� ���ʿ� �����
        // ���� ������ ������Ʈ�� Queue������ Dequeue�� �ذᰡ��
        // ���� �������� ������Ʈ��?

        
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
