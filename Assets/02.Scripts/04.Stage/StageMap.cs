using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MVP���Ŀ� Stage Class�� ���� ����
public class StageMap : MonoBehaviour
{
    [SerializeField] private StageNameType stageName;
    public BoxCollider SpawnArea; //�ش� ���������� Enemy�� ��ȯ�� ����

    public StageNameType StageName { get => stageName; }
}
