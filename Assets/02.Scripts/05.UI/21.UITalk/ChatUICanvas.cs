using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatUICanvas : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image CharacterImage;
    [SerializeField] private Text CharacterName;
    [SerializeField] private Text CharacterText;
    [SerializeField] private Button Skip;

    private bool isPlay;
    private bool isConfirm;
    private bool isSkip;

    private WaitUntil click;

    private void Start()
    {
        click = new WaitUntil(() =>
        {
            if (isSkip == true) return true;
            return isConfirm;
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isConfirm = true;
    }

    public void PrintDialogue(int cycle, int index)
    {
        var audioSource = ObjectPoolManager.Instance.GetPool(Const.AUDIO_SOURCE_KEY, Const.AUDIO_SOURCE_POOL_KEY).GetObject();
        audioSource.SetActive(true);
        AudioSource audio = audioSource.GetComponent<AudioSource>();
        audio.Play();
    }

    private IEnumerator StartTalk()
    {
        while(isPlay == true)
        {

            isConfirm = false;
            yield return click;
            yield return null;
        }
    }
}
