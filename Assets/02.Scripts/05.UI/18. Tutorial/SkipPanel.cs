using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkipPanel : MonoBehaviour
{
    [SerializeField] private Button confirm;
    [SerializeField] private Button cancel;
    public bool isDone;

    private void Start()
    {
        confirm.onClick.AddListener(() =>
        {
            isDone = true;
        });

        cancel.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });

    }
    
}