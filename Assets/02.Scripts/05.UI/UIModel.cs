using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IUIBase
{
    void Initialize();
    void ShowUI();
    void HideUI();
    void UpdateUI();
}

public class UIModel : MonoBehaviour
{
    // 상태 관리
    // 공통 데이터
}
