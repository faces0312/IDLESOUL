using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public int ID;
    private string stageName;

    [SerializeField] private TextMeshProUGUI Name;

    private Button button;
    private bool isOpen;

    private void OnEnable()
    {
        Name.text = stageName;
    }

    public void SetStageName(string stageName)
    {
        this.stageName = stageName;
    }
}