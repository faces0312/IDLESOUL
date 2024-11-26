using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UIBase
{
    void Initialize();
    void ShowUI();
    void HideUI();
    void UpdateUI();
}

public class UIView : MonoBehaviour, UIBase
{
    public virtual void Initialize()
    {
    }

    public virtual void HideUI()
    {
    }

    public virtual void ShowUI()
    {
    }

    public virtual void UpdateUI()
    {
    }
}
