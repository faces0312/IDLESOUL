using UnityEngine;
using Enums;
using System.Collections.Generic;

public class StageSelector : MonoBehaviour
{
    [SerializeField] private RectTransform content;

    private List<StageDB> stageDatas;
    private List<StageDB> targetStageDBs;
    private List<Stage> stages;
    private StageType stageType;
    private ObjectPool stagePool;

    private readonly int POOL_KEY = 0;
    private readonly string DICT_KEY = "StagePool";
    private readonly string STAGE_PREFAB_PATH = "Prefabs/Sample/Stage";

    private void Awake()
    {
        stageDatas = DataManager.Instance.StageDB.ItemsList;
        stagePool = new ObjectPool(POOL_KEY, 10, STAGE_PREFAB_PATH, content);
        ObjectPoolManager.Instance.AddPool(DICT_KEY, stagePool);
    }

    private void OnEnable()
    {
        foreach (StageDB item in stageDatas)
        {
            if ((StageType)item.StageType == stageType)
            {
                targetStageDBs.Add(item);
            }
        }
    }

    public void SetStageType(StageType type)
    {
        this.stageType = type;
    }
}
