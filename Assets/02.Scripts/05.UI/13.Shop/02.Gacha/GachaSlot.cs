using UnityEngine;
using UnityEngine.UI;

public class GachaSlot : MonoBehaviour
{
    public int key;
    public Image icon;

    public void SetContentItem(int key)
    {
        if (key == 0) return;
        this.key = key;
        icon.sprite = Resources.Load<Sprite>(DataManager.Instance.ItemDB.GetByKey(key).IconPath);
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
    }
}
