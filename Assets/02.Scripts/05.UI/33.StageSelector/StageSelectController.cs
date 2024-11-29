using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{
    public StageSelector StageSelector;

    [SerializeField] private Button daily;
    [SerializeField] private Button exp;
    [SerializeField] private Button upgrade;

    private void Awake()
    {
        StageSelector = GetComponent<StageSelector>();
        daily.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.Daily);
            StageSelector.gameObject.SetActive(true);
        });
        exp.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.EXP);
            StageSelector.gameObject.SetActive(true);
        });
        upgrade.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.Upgrade);
            StageSelector.gameObject.SetActive(true);
        });
    }
}