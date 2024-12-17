using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IGachableDB
{
    public int GetKey();
    public int GetRairity();
}
public class GachaBase : MonoBehaviour
{
    public GachaResult result;
    public GachaGrid grid;
    public GameObject gachaBase;

    private GachaController controller;

    private List<IGachableDB> gachaList;
    private List<IGachableDB> tempList;
    private List<ItemDB> items;
    private List<SoulDB> souls;

    private void Start()
    {
        controller = new GachaController();
        controller.GachaPanel = gachaBase;
        UIManager.Instance.RegisterController("gachaController", controller);

        items = DataManager.Instance.ItemDB.ItemsList;
        souls = DataManager.Instance.SoulDB.ItemsList;
        gachaList = new List<IGachableDB>();
        tempList = new List<IGachableDB>();
        
        EventManager.Instance.Subscribe<GachaEvent>(Channel.Gacha, Gacha);
        gachaBase.SetActive(false);
    }


    private void Gacha(GachaEvent arg)
    {
        if(grid.gameObject.activeSelf == true)
        {
            grid.gameObject.SetActive(false);   
        }
        if (this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }
        switch (arg.type)
        {
            case GachaType.Weapon:
                gachaList.Clear();
                foreach(ItemDB item in DataManager.Instance.ItemDB.ItemsList)
                {
                    gachaList.Add(item);
                }
                break;
            case GachaType.Soul:
                gachaList.Clear();
                foreach (SoulDB item in DataManager.Instance.SoulDB.ItemsList)
                {
                    gachaList.Add(item);
                }
                break;
        }
        tempList.Clear();
        for(int i = 0; i < arg.count; i++)
        {
            int rand = Random.Range(0, gachaList.Count);
            tempList.Add(gachaList[rand]);
        }
        result.SetList(tempList);
        if(result.coResult != null)
        {
            StopCoroutine(result.coResult);
            result.coResult = null;
        }
        result.coResult = StartCoroutine(result.CoResult());
    }
}
