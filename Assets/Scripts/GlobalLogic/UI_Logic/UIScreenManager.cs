using UnityEngine;
using System.Collections.Generic;

public class UIScreenManager : MonoBehaviour
{
    [Header("Screens (UI ������)")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject screen1;
    [SerializeField] private GameObject screen2;
    [SerializeField] private GameObject screen3;

    // ���� ��� �������� ������� �������
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
            Debug.Log("���������� ������ '�����' ������");
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
        Debug.Log("������ �����: " + currentScreen.name);
    }

    // ��������� ������ ��� �������� ���������� �������,
    // �� ����� ���� ��������� � UI-������� ����� OnClick.
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
            Debug.Log("��������� �� �����: " + currentScreen.name);
        }
        else
        {
            if (currentScreen != mainMenu)
            {
                currentScreen.SetActive(false);
                currentScreen = mainMenu;
                currentScreen.SetActive(true);
                Debug.Log("������� �����. ������ ������� �����");
            }
        }
    }

    [Header("Scene Transition Settings")]
    [SerializeField] private string gameSceneName = "GameScene";
    public void LoadGameScene()
    {
        Debug.Log("������� �� �����: " + gameSceneName);
        //SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("����� �� ����.");
        Application.Quit();
    }
}
