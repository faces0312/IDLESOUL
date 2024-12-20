using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UIConfig : MonoBehaviour
{
    public string UIKey; //"ConfigController"

    private ConfigModel model;
    [SerializeField] private ConfigView view;
    private ConfigController controller;

    private void Start()
    {
        //Model(Data) 초기화
        model = new ConfigModel();

        //컨트롤러  초기화 및 View 등록
        controller = new ConfigController();
        controller.Initialize(view, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
        gameObject.SetActive(false);
    }

}