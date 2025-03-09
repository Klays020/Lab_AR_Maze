using UnityEngine;
using UnityEngine.UI;

public class UISliderAudio : MonoBehaviour
{
    private enum SliderType
    {
        MusicVolume,
        EffectsVolume
    }

    [SerializeField] private SliderType sliderType;
    private AudioManager audioManager;
    private Slider slider;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        slider = GetComponent<Slider>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager не найден в сцене!");
            return;
        }

        if (slider)
        {
            slider.value = sliderType == SliderType.MusicVolume ? audioManager.musicVolume : audioManager.effectsVolume;
            slider.onValueChanged.AddListener(UpdateVolume);
        }
    }

    private void UpdateVolume(float value)
    {
        if (audioManager == null) return;

        switch (sliderType)
        {
            case SliderType.MusicVolume:
                audioManager.musicVolume = value;
                break;
            case SliderType.EffectsVolume:
                audioManager.effectsVolume = value;
                break;
        }
    }
}
