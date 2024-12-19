using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScene : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneDataManager.Instance.LoadScene("GameScene_SMS");
    }
}