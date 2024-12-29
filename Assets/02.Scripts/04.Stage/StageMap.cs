using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MVP이후에 Stage Class와 병합 예정
public class StageMap : MonoBehaviour
{
    [SerializeField] private StageNameType stageName;
    public BoxCollider SpawnArea; //해당 스테이지의 Enemy가 소환될 영역

    public StageNameType StageName { get => stageName; }
}
