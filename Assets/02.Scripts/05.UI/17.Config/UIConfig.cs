using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UIConfig : UIBase<ConfigModel, ConfigView, ConfigController>
{

    public override void Start()
    {
        //Model(Data) 초기화
        model = new ConfigModel();
        base.Start();
        gameObject.SetActive(false);
    }

}