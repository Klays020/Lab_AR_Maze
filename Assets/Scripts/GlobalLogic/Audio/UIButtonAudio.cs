using UnityEngine;
using UnityEngine.UI;

public class UIButtonAudio : MonoBehaviour
{
    private enum SoundType
    {
        Click,
        Back,
        Difficult,
        UpdateMusicVolume,
        UpdateEffectsVolume
    }

    [SerializeField] private SoundType soundType;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager не найден в сцене!");
            return;
        }

        Button button = GetComponent<Button>();
        if (button)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        if (audioManager == null) return;

        switch (soundType)
        {
            case SoundType.Click:
                audioManager.PlayButtonClick();
                break;
            case SoundType.Back:
                audioManager.PlayButtonClickBack();
                break;
            case SoundType.Difficult:
                audioManager.PlayButtonClickDifficult();
                break;
            case SoundType.UpdateMusicVolume:
                audioManager.PlayButtonClickDifficult();
                break;
            case SoundType.UpdateEffectsVolume:
                audioManager.PlayButtonClickDifficult();
                break;
        }
    }
}
