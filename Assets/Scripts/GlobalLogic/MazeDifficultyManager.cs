using UnityEngine;
using TMPro;

public class MazeDifficultyManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown difficultyDropdown;

    private void Start()
    {
        if (difficultyDropdown != null)
        {
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        }
    }

    private void OnDifficultyChanged(int index)
    {
        switch (index)
        {
            case 0:
                GameSettings.MazeRows = 5;
                GameSettings.MazeColumns = 5;
                GameSettings.selectedDifficulty = Difficulty.Easy;
                break;
            case 1:
                GameSettings.MazeRows = 7;
                GameSettings.MazeColumns = 7;
                GameSettings.selectedDifficulty = Difficulty.Medium;
                break;
            case 2:
                GameSettings.MazeRows = 10;
                GameSettings.MazeColumns = 10;
                GameSettings.selectedDifficulty = Difficulty.Hard;
                break;
            default:
                GameSettings.MazeRows = 5;
                GameSettings.MazeColumns = 5;
                GameSettings.selectedDifficulty = Difficulty.Easy;
                break;
        }
        Debug.Log("Сложность установлена: " + difficultyDropdown.options[index].text +
                  " (Rows: " + GameSettings.MazeRows + ", Columns: " + GameSettings.MazeColumns + ")");
    }

    public void OnLabyrinthGenerated()
    {
        if (difficultyDropdown != null)
            difficultyDropdown.interactable = false;
        Debug.Log("Лабиринт сгенерирован. Сложность заблокирована.");
    }
}
