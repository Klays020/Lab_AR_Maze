using UnityEngine;
using TMPro;

public class StarRatingDisplay : MonoBehaviour
{
    [Header("Star GameObjects")]
    [Tooltip("������ ��� 0 ����� (��������, ������ ��������� ��� �����������, ���������� ������)")]
    [SerializeField] private GameObject star0;
    [Tooltip("������ ��� 1 ������")]
    [SerializeField] private GameObject star1;
    [Tooltip("������ ��� 2 �����")]
    [SerializeField] private GameObject star2;
    [Tooltip("������ ��� 3 �����")]
    [SerializeField] private GameObject star3;

    [Header("Score Thresholds")]
    [Tooltip("����������� ���� ��� 1 ������")]
    [SerializeField] private int threshold1 = 30;
    [Tooltip("����������� ���� ��� 2 �����")]
    [SerializeField] private int threshold2 = 40;
    [Tooltip("����������� ���� ��� 3 �����")]
    [SerializeField] private int threshold3 = 60;

    public void UpdateStarRating(int score)
    {
        // ������� ������������ ��� ������� �����
        if (star0 != null) star0.SetActive(false);
        if (star1 != null) star1.SetActive(false);
        if (star2 != null) star2.SetActive(false);
        if (star3 != null) star3.SetActive(false);

        if (score < threshold1)
        {
            star0.SetActive(true);
        }

        if (score >= threshold3)
        {
            if (star3 != null) star3.SetActive(true);
        }
        else if (score >= threshold2)
        {
            if (star2 != null) star2.SetActive(true);
        }
        else if (score >= threshold1)
        {
            if (star1 != null) star1.SetActive(true);
        }
    }

    // ��������� �������� ����� �� PlayerPrefs ��� �������� ��������� � ��������� ������.
    public void UpdateStarRatingFromPrefs(string difficulty)
    {
        string key = "BestScore_" + difficulty;

        if (PlayerPrefs.HasKey(key))
        {
            int score = PlayerPrefs.GetInt(key);
            Debug.Log($"������ ���� {score} ��� ��������� {difficulty}");
            UpdateStarRating(score);
        }
        else
        {
            Debug.Log($"��� ������������ ����� ��� ��������� {difficulty}. ������ �� ���������.");
            UpdateStarRating(0);
        }
    }
    public void UpdateLastStarRatingFromPrefs(string difficulty)
    {
        string key = "LastScore_" + difficulty;
        if (PlayerPrefs.HasKey(key))
        {
            int score = PlayerPrefs.GetInt(key);
            Debug.Log($"������ ��������� ���� {score} ��� ��������� {difficulty}");
            UpdateStarRating(score);
        }
        else
        {
            Debug.Log($"��� ������������ ���������� ����� ��� ��������� {difficulty}. ������ �� ���������.");
            UpdateStarRating(0);
        }
    }
}
