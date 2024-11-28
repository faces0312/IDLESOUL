using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private Button button;
    private string key;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        key = button.name;
    }
}