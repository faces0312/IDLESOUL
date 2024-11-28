using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;// 확장성을 위해서 추가만함, 사용X
    public int DropGoldMin; // 게임 재화 골드 최소
    public int DropGoldMax; // 게임 재화 골드 최대
    public int DropDiamondMin;  // 가챠 재화 최소
    public int DropDiamondMax;  // 가챠 재화 최대

    public StatHandler statHandler; //스텟 관리 클래스

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
