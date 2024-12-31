using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneDataManager : SingletonDDOL<SceneDataManager>
{
    public string NextScene;
    public float Modifier;
    public float MainStageModifier; //���� �������� ���� �� ������ �� ���갪
    public int Chapter; //11�������� ���� �� é��++ ���������� 1�� �ʱ�ȭ
    public int Stage; //���� �������� ���� ��

    protected override void Awake()
    {
        base.Awake();
        MainStageModifier = 1;
        Chapter = 1;
    }   

    public void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneManager.LoadScene("LoadingScene");
        SoundManager.Instance.ChangeBGMForScene("LoadingScene");
    }

    public void SaveSceneData()
    {
        //Todo : ���� �� �� ����, �ε� �� Stage���� ���� ���� ���ٸ� modifier�� �߰��� ������ ����
    }

}