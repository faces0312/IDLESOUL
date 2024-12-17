using UnityEngine;
using UnityEngine.UI;

public class ConfigView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject ConfigPanel;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider mainVolume;
    [SerializeField] private Slider bgmVolume;
    [SerializeField] private Slider sfxVolume;
    private ConfigController controller;

    public Toggle fps30;
    public Toggle fps60;

    private void Start()
    {
        controller = new ConfigController();
        UIManager.Instance.RegisterController(controller.key, controller);

        exitButton.onClick.AddListener(() =>
        {
            ConfigPanel.SetActive(false);
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

        ConfigPanel.SetActive(false);
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