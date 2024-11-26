using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadTest : MonoBehaviour
{
    public void LoadSceneButton()
    {
        SceneDataManager.Instance.LoadScene("TestScene_PDS");
    }
}
