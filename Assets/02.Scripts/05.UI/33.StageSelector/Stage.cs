using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public int ID;

    [SerializeField] private TextMeshProUGUI Name;

    private Button button;
    private string stageName;
    private bool isOpen;

    private void OnEnable()
    {
        Name.text = stageName;
    }
}