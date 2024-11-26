using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController
{
    protected UIBase view;
    protected UIModel model;

    public virtual void Initialize(UIBase view, UIModel model)
    {
        this.view = view;
        this.model = model;

        view.Initialize();              // View가 생성될 때 호출
    }

    public abstract void OnShow();      // View가 활성화될 때 호출
    public abstract void OnHide();      // View가 비활성화될 때 호출
    public abstract void UpdateView();  // Model 변경 시 View 갱신
}
