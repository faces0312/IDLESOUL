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
        shop.onClick.AddListener(() =>
        {
            shopObject.SetActive(true);
        });
    }
}
