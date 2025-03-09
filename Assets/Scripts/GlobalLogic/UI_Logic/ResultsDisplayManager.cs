using UnityEngine;
using TMPro;

public class ResultsDisplayManager : MonoBehaviour
{
    [Header("Easy Results")]
    [SerializeField] private TextMeshProUGUI easyBestTimeText;  // Лучшее время для Easy
    [SerializeField] private TextMeshProUGUI easyBestScoreText; // Лучший счет для Easy
    [SerializeField] private TextMeshProUGUI easyLastTimeText;  // Последнее время для Easy
    [SerializeField] private TextMeshProUGUI easyLastScoreText; // Последний счет для Easy

    [Header("Medium Results")]
    [SerializeField] private TextMeshProUGUI mediumBestTimeText;
    [SerializeField] private TextMeshProUGUI mediumBestScoreText;
    [SerializeField] private TextMeshProUGUI mediumLastTimeText;
    [SerializeField] private TextMeshProUGUI mediumLastScoreText;

    [Header("Hard Results")]
    [SerializeField] private TextMeshProUGUI hardBestTimeText;
    [SerializeField] private TextMeshProUGUI hardBestScoreText;
    [SerializeField] private TextMeshProUGUI hardLastTimeText;
    [SerializeField] private TextMeshProUGUI hardLastScoreText;

    private void Start()
    {
        UpdateResults();
    }

    public void UpdateResults()
    {
        // Easy результаты
        if (PlayerPrefs.HasKey("BestTime_Easy"))
            easyBestTimeText.text = PlayerPrefs.GetFloat("BestTime_Easy").ToString("F2");
        else
            easyBestTimeText.text = "";

        if (PlayerPrefs.HasKey("BestScore_Easy"))
            easyBestScoreText.text = PlayerPrefs.GetInt("BestScore_Easy").ToString();
        else
            easyBestScoreText.text = "";

        if (PlayerPrefs.HasKey("LastTime_Easy"))
            easyLastTimeText.text = PlayerPrefs.GetFloat("LastTime_Easy").ToString("F2");
        else
            easyLastTimeText.text = "";

        if (PlayerPrefs.HasKey("LastScore_Easy"))
            easyLastScoreText.text = PlayerPrefs.GetInt("LastScore_Easy").ToString();
        else
            easyLastScoreText.text = "";

        // Medium результаты
        if (PlayerPrefs.HasKey("BestTime_Medium"))
            mediumBestTimeText.text = PlayerPrefs.GetFloat("BestTime_Medium").ToString("F2");
        else
            mediumBestTimeText.text = "";

        if (PlayerPrefs.HasKey("BestScore_Medium"))
            mediumBestScoreText.text = PlayerPrefs.GetInt("BestScore_Medium").ToString();
        else
            mediumBestScoreText.text = "";

        if (PlayerPrefs.HasKey("LastTime_Medium"))
            mediumLastTimeText.text = PlayerPrefs.GetFloat("LastTime_Medium").ToString("F2");
        else
            mediumLastTimeText.text = "";

        if (PlayerPrefs.HasKey("LastScore_Medium"))
            mediumLastScoreText.text = PlayerPrefs.GetInt("LastScore_Medium").ToString();
        else
            mediumLastScoreText.text = "";

        // Hard результаты
        if (PlayerPrefs.HasKey("BestTime_Hard"))
            hardBestTimeText.text = PlayerPrefs.GetFloat("BestTime_Hard").ToString("F2");
        else
            hardBestTimeText.text = "";

        if (PlayerPrefs.HasKey("BestScore_Hard"))
            hardBestScoreText.text = PlayerPrefs.GetInt("BestScore_Hard").ToString();
        else
            hardBestScoreText.text = "";

        if (PlayerPrefs.HasKey("LastTime_Hard"))
            hardLastTimeText.text = PlayerPrefs.GetFloat("LastTime_Hard").ToString("F2");
        else
            hardLastTimeText.text = "";

        if (PlayerPrefs.HasKey("LastScore_Hard"))
            hardLastScoreText.text = PlayerPrefs.GetInt("LastScore_Hard").ToString();
        else
            hardLastScoreText.text = "";
    }
}
