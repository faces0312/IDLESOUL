using UnityEngine;

public class GachaResultController : UIController
{
    [SerializeField] private GameObject GachaPanel;

    public override void OnHide()
    {
        GachaPanel.SetActive(false);
    }

    public override void OnShow()
    {
        GachaPanel.SetActive(true);
    }

    public override void UpdateView()
    {
        
    }
}