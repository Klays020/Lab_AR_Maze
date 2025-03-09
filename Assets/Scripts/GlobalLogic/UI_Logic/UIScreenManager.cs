using UnityEngine;
using System.Collections.Generic;

public class UIScreenManager : MonoBehaviour
{
    [Header("Screens (UI панели)")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject screen1;
    [SerializeField] private GameObject screen2;
    [SerializeField] private GameObject screen3;

    // Стек для хранения истории экранов
    private Stack<GameObject> screenHistory = new Stack<GameObject>();

    private GameObject currentScreen;

    private void Start()
    {
        OpenScreen(mainMenu, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Аппаратная кнопка 'Назад' нажата");
            GoBack();
        }
    }

    public void OpenScreen(GameObject newScreen, bool pushCurrent = true)
    {
        if (pushCurrent && currentScreen != null)
        {
            screenHistory.Push(currentScreen);
            currentScreen.SetActive(false);
        }
        currentScreen = newScreen;
        currentScreen.SetActive(true);
        Debug.Log("Открыт экран: " + currentScreen.name);
    }

    // Публичные методы для открытия конкретных экранов,
    // Их можно было привязать к UI-кнопкам через OnClick.
    public void OpenScreen1() { OpenScreen(screen1); }
    public void OpenScreen2() { OpenScreen(screen2); }
    public void OpenScreen3() { OpenScreen(screen3); }
    public void OpenMainMenu() { OpenScreen(mainMenu, false); }

    public void GoBack()
    {
        if (screenHistory.Count > 0)
        {
            currentScreen.SetActive(false);
            currentScreen = screenHistory.Pop();
            currentScreen.SetActive(true);
            Debug.Log("Вернулись на экран: " + currentScreen.name);
        }
        else
        {
            if (currentScreen != mainMenu)
            {
                currentScreen.SetActive(false);
                currentScreen = mainMenu;
                currentScreen.SetActive(true);
                Debug.Log("История пуста. Открыт главный экран");
            }
        }
    }

    [Header("Scene Transition Settings")]
    [SerializeField] private string gameSceneName = "GameScene";
    public void LoadGameScene()
    {
        Debug.Log("Переход на сцену: " + gameSceneName);
        //SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры.");
        Application.Quit();
    }
}
