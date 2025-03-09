using UnityEngine;
using TMPro;

public class StarRatingDisplay : MonoBehaviour
{
    [Header("Star GameObjects")]
    [Tooltip("Объект для 0 звезд (например, пустой контейнер или изображение, скрывающее звезды)")]
    [SerializeField] private GameObject star0;
    [Tooltip("Объект для 1 звезды")]
    [SerializeField] private GameObject star1;
    [Tooltip("Объект для 2 звезд")]
    [SerializeField] private GameObject star2;
    [Tooltip("Объект для 3 звезд")]
    [SerializeField] private GameObject star3;

    [Header("Score Thresholds")]
    [Tooltip("Минимальный счет для 1 звезды")]
    [SerializeField] private int threshold1 = 30;
    [Tooltip("Минимальный счет для 2 звезд")]
    [SerializeField] private int threshold2 = 40;
    [Tooltip("Минимальный счет для 3 звезд")]
    [SerializeField] private int threshold3 = 60;

    public void UpdateStarRating(int score)
    {
        // Сначала деактивируем все объекты звезд
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

    // Считывает значение счета из PlayerPrefs для заданной сложности и обновляет звезды.
    public void UpdateStarRatingFromPrefs(string difficulty)
    {
        string key = "BestScore_" + difficulty;

        if (PlayerPrefs.HasKey(key))
        {
            int score = PlayerPrefs.GetInt(key);
            Debug.Log($"Считан счет {score} для сложности {difficulty}");
            UpdateStarRating(score);
        }
        else
        {
            Debug.Log($"Нет сохраненного счета для сложности {difficulty}. Звезды не обновлены.");
            UpdateStarRating(0);
        }
    }
    public void UpdateLastStarRatingFromPrefs(string difficulty)
    {
        string key = "LastScore_" + difficulty;
        if (PlayerPrefs.HasKey(key))
        {
            int score = PlayerPrefs.GetInt(key);
            Debug.Log($"Считан последний счет {score} для сложности {difficulty}");
            UpdateStarRating(score);
        }
        else
        {
            Debug.Log($"Нет сохраненного последнего счета для сложности {difficulty}. Звезды не обновлены.");
            UpdateStarRating(0);
        }
    }
}
