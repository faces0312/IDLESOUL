using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UICurGainKeyCount : UIBase<UICurGainKeyCountModel, UICurGainKeyCountView, UICurGainKeyCountController>
{
    public override void Start()
    {
        model = new UICurGainKeyCountModel();
        base.Start();
        controller.OnShow();
    }
}
