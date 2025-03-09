using UnityEngine;

public class FinalOutcomeSelector : MonoBehaviour
{
    [Header("Victory Screens")]
    [Tooltip("����� ������ ��� ��������� Easy")]
    [SerializeField] private GameObject victoryEasy;
    [Tooltip("����� ������ ��� ��������� Medium")]
    [SerializeField] private GameObject victoryMedium;
    [Tooltip("����� ������ ��� ��������� Hard")]
    [SerializeField] private GameObject victoryHard;

    [Header("Defeat Screen")]
    [Tooltip("����� ���������")]
    [SerializeField] private GameObject defeatScreen;

    public void ActivateOutcome(bool isVictory)
    {
        // ������������ ��� ������
        if (victoryEasy != null) victoryEasy.SetActive(false);
        if (victoryMedium != null) victoryMedium.SetActive(false);
        if (victoryHard != null) victoryHard.SetActive(false);
        if (defeatScreen != null) defeatScreen.SetActive(false);

        if (isVictory)
        {
            string diff = GameSettings.selectedDifficulty.ToString();
            Debug.Log("��������� ���������: " + diff);

            if (diff == "Easy" && victoryEasy != null)
            {
                victoryEasy.SetActive(true);
                Debug.Log("����������� ����� ������ ��� Easy");
            }
            else if (diff == "Medium" && victoryMedium != null)
            {
                victoryMedium.SetActive(true);
                Debug.Log("����������� ����� ������ ��� Medium");
            }
            else if (diff == "Hard" && victoryHard != null)
            {
                victoryHard.SetActive(true);
                Debug.Log("����������� ����� ������ ��� Hard");
            }
        }
        else
        {
            if (defeatScreen != null)
            {
                defeatScreen.SetActive(true);
                Debug.Log("����������� ����� ���������");
            }
        }
    }
}
