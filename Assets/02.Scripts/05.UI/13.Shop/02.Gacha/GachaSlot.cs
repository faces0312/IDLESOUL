using UnityEngine;
using UnityEngine.UI;

public class GachaSlot : MonoBehaviour
{
    public int key;
    public Image icon;
    public Image border;

    public void SetContentItem(int key)
    {
        if (key == 0) return;
        this.key = key;
        icon.sprite = Resources.Load<Sprite>(DataManager.Instance.ItemDB.GetByKey(key).IconPath);
        SetBorderColor(DataManager.Instance.ItemDB.GetByKey(key).GetRairity());
    }

    public void SetContentSoul(int key)
    {
        if (key == 0) return;
        this.key = key;
        string path = DataManager.Instance.SoulDB.GetByKey(key).IconPath;
        if (path != null)
            icon.sprite = Resources.Load<Sprite>(path);
        else icon.sprite = null;
    }

    public void ClearSlot()
    {
        this.key = 0;
        this.icon.sprite = null;
        this.border.color = Color.white;
    }

    public void SetBorderColor(Enums.Rairity rairity)
    {
        switch(rairity)
        {
            case Enums.Rairity.Normal:
                this.border.color = Color.white; break;
            case Enums.Rairity.Rare:
                this.border.color = Color.green; break;
            case Enums.Rairity.Epic:
                this.border.color = Color.magenta; break;
            case Enums.Rairity.Legendary:
                this.border.color = Color.yellow; break;
        }
    }
}
