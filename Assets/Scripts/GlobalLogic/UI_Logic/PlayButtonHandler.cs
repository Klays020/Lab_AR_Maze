using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonHandler : MonoBehaviour
{
    public static PlayButtonHandler Instance { get; private set; }

    [SerializeField] private string gameSceneName = "GameScene";
    private Button playButton;

    private void Awake()
    {
        Instance = this;
        playButton = GetComponent<Button>();

        if (playButton != null)
            playButton.interactable = false;
    }

    public void SetInteractable(bool value)
    {
        if (playButton != null)
            playButton.interactable = value;
    }

    public void OnPlayButtonClicked()
    {
        switch (GameSettings.selectedDifficulty)
        {
            case Difficulty.Easy:
                GameSettings.MazeRows = 5;
                GameSettings.MazeColumns = 5;
                break;
            case Difficulty.Medium:
                GameSettings.MazeRows = 7;
                GameSettings.MazeColumns = 7;
                break;
            case Difficulty.Hard:
                GameSettings.MazeRows = 10;
                GameSettings.MazeColumns = 10;
                break;
            default:
                GameSettings.MazeRows = 5;
                GameSettings.MazeColumns = 5;
                break;
        }

        Debug.Log("Нажата кнопка Play. Сложность: " + GameSettings.selectedDifficulty +
                  ", Rows: " + GameSettings.MazeRows + ", Columns: " + GameSettings.MazeColumns);

        SceneManager.LoadScene(gameSceneName);
    }
}
