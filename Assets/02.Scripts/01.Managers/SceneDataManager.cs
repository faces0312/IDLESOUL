using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneDataManager : SingletonDDOL<SceneDataManager>
{
    public string NextScene;
    public float Modifier;
    public float MainStageModifier; //메인 스테이지 진행 중 배율의 총 연산값
    public int Chapter; //11스테이지 도달 시 챕터++ 스테이지는 1로 초기화
    public int Stage; //현재 스테이지 저장 값

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
        //Todo : 현재 씬 값 저장, 로드 시 Stage값이 저장 값과 같다면 modifier에 추가로 곱하지 않음
    }

}