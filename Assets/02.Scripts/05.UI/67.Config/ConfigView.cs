using UnityEngine;
using UnityEngine.UI;

public class ConfigView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject ConfigPanel;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider mainVolume;
    [SerializeField] private Slider bgmVolume;
    [SerializeField] private Slider sfxVolume;

    public ConfigController controller;

    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            HideUI();
        });

        mainVolume.onValueChanged.AddListener((float value) =>
        {
            SoundManager.Instance.SetMasterVolume(value);
        });

        bgmVolume.onValueChanged.AddListener((float value) =>
        {
            SoundManager.Instance.SetBGMVolume(value);
        });

        sfxVolume.onValueChanged.AddListener((float value) =>
        {
            SoundManager.Instance.SetSoundEffectVolume(value);
        });
    } 

    public void HideUI()
    {
        ConfigPanel.SetActive(false);
    }

    public void Initialize()
    {
        
    }

    public void ShowUI()
    {
        ConfigPanel.SetActive(true);
    }

    public void UpdateUI()
    {
        
        
        
    }
}