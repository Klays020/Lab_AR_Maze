using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultyDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject easyDisplay;
    [SerializeField] private GameObject mediumDisplay;
    [SerializeField] private GameObject hardDisplay;


    private void Start()
    {
        SetDifficulty(GameSettings.selectedDifficulty);
    }
    public void SetDifficulty(Difficulty difficulty)
    {
        easyDisplay.SetActive(difficulty == Difficulty.Easy);
        mediumDisplay.SetActive(difficulty == Difficulty.Medium);
        hardDisplay.SetActive(difficulty == Difficulty.Hard);
    }
}
