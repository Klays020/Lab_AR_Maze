using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    [SerializeField] public float musicVolume = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] public float effectsVolume = 0.5f;

    [Header("Audio Clips")]
    [Tooltip("Клип для кнопок в главном меню")]
    [SerializeField] private AudioClip buttonClickClip;
    [Tooltip("Клип для кнопки назад в главном меню")]
    [SerializeField] private AudioClip buttonClickClipBack;
    [Tooltip("Клип для кнопок сложности в главном меню")]
    [SerializeField] private AudioClip buttonClickClipDifficult;
    [Tooltip("Клип для фоновой музыки главного меню")]
    [SerializeField] private AudioClip mainMenuMusic;
    [Tooltip("Клип для фоновой музыки игры")]
    [SerializeField] private AudioClip gameSceneMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // сохраняем объект при смене сцен
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        UpdateMusicVolume(musicVolume);
        UpdateEffectsVolume(effectsVolume);
    }

    private void Update()
    {
        UpdateMusicVolume(musicVolume);
        UpdateEffectsVolume(effectsVolume);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            ChangeMusic(mainMenuMusic);
        }
        else if (scene.name == "GameScene")
        {
            ChangeMusic(gameSceneMusic);
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void UpdateMusicVolume(float volume)
    {
        musicVolume = volume;
        if (musicSource != null)
            musicSource.volume = volume;
    }

    public void UpdateEffectsVolume(float volume)
    {
        effectsVolume = volume;
        if (effectsSource != null)
            effectsSource.volume = volume;
    }

    public void ChangeMusic(AudioClip newClip, bool playImmediately = true)
    {
        if (musicSource != null && newClip != null)
        {
            musicSource.clip = newClip;
            if (playImmediately)
                musicSource.Play();
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        if (effectsSource != null && clip != null)
        {
            effectsSource.PlayOneShot(clip, effectsVolume);
        }
    }

    public void PlayButtonClick()
    {
        PlayEffect(buttonClickClip);
    }
    public void PlayButtonClickBack()
    {
        PlayEffect(buttonClickClipBack);
    }
    public void PlayButtonClickDifficult()
    {
        PlayEffect(buttonClickClipDifficult);
    }
}
