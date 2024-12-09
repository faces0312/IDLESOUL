using UnityEngine;

public class GameSceneTigger : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.StartGame();
    }
}