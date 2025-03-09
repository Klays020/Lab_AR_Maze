using UnityEngine;

public class FinalOutcomeSelector : MonoBehaviour
{
    [Header("Victory Screens")]
    [Tooltip("Ёкран победы дл€ сложности Easy")]
    [SerializeField] private GameObject victoryEasy;
    [Tooltip("Ёкран победы дл€ сложности Medium")]
    [SerializeField] private GameObject victoryMedium;
    [Tooltip("Ёкран победы дл€ сложности Hard")]
    [SerializeField] private GameObject victoryHard;

    [Header("Defeat Screen")]
    [Tooltip("Ёкран поражени€")]
    [SerializeField] private GameObject defeatScreen;

    public void ActivateOutcome(bool isVictory)
    {
        // ƒеактивируем все экраны
        if (victoryEasy != null) victoryEasy.SetActive(false);
        if (victoryMedium != null) victoryMedium.SetActive(false);
        if (victoryHard != null) victoryHard.SetActive(false);
        if (defeatScreen != null) defeatScreen.SetActive(false);

        if (isVictory)
        {
            string diff = GameSettings.selectedDifficulty.ToString();
            Debug.Log("¬ыбранна€ сложность: " + diff);

            if (diff == "Easy" && victoryEasy != null)
            {
                victoryEasy.SetActive(true);
                Debug.Log("јктивирован экран победы дл€ Easy");
            }
            else if (diff == "Medium" && victoryMedium != null)
            {
                victoryMedium.SetActive(true);
                Debug.Log("јктивирован экран победы дл€ Medium");
            }
            else if (diff == "Hard" && victoryHard != null)
            {
                victoryHard.SetActive(true);
                Debug.Log("јктивирован экран победы дл€ Hard");
            }
        }
        else
        {
            if (defeatScreen != null)
            {
                defeatScreen.SetActive(true);
                Debug.Log("јктивирован экран поражени€");
            }
        }
    }
}
