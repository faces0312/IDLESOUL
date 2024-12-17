using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISideButtonController : MonoBehaviour
{
    [SerializeField] private Button menu;
    [SerializeField] private GameObject menuObject;

    [SerializeField] private Button shop;
    [SerializeField] private GameObject shopObject;
    void Start()
    {
        menu.onClick.AddListener(() =>
        {
            if (menuObject.activeSelf == true)
            {
                menuObject.SetActive(false);
            }
            else menuObject.SetActive(true);
        });
        //shop.onClick.AddListener(() =>
        //{
        //    shopObject.SetActive(true); //Debug - Shop UI 매니저 등록하면 변경하여 사용하기
        //});
    }
}
