using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float sizeIncrease = 15f;

    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color dangerColor = Color.red;

    private float maxTime; // ћаксимальное врем€ дл€ текущей сложности
    private float defaultFontSize;

    private void Start()
    {
        defaultFontSize = timerText.fontSize;

        // ѕытаемс€ сразу получить значение из ScoreManager
        if (ScoreManager.Instance != null && ScoreManager.Instance.startCountdownTime > 0)
        {
            maxTime = ScoreManager.Instance.startCountdownTime;
        }
        else
        {
            maxTime = 0;
            Debug.Log("maxTime = 0, ожидаем инициализацию ScoreManager.");
            timerText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // ≈сли maxTime еще не установлен, пробуем обновить его из ScoreManager
        if (maxTime <= 0 && ScoreManager.Instance != null && ScoreManager.Instance.startCountdownTime > 0)
        {
            maxTime = ScoreManager.Instance.startCountdownTime;
            Debug.Log("maxTime обновлен: " + maxTime);
            timerText.gameObject.SetActive(true);
        }

        if (ScoreManager.Instance != null)
        {
            float time = ScoreManager.Instance.countdownTime;
            timerText.text = Mathf.CeilToInt(time).ToString();

            // »спользуем maxTime дл€ расчета процентов оставшегос€ времени
            float timePercentage = (maxTime > 0) ? (time / maxTime) : 1f; // если maxTime еще 0, считаем, что 100% времени

            if (timePercentage <= 0.2f)
            {
                timerText.color = dangerColor;
                timerText.fontSize = defaultFontSize + sizeIncrease * 2;
            }
            else if (timePercentage <= 0.5f)
            {
                timerText.color = warningColor;
                timerText.fontSize = defaultFontSize + sizeIncrease;
            }
            else
            {
                timerText.color = defaultColor;
                timerText.fontSize = defaultFontSize;
            }
        }
    }

    public void ResetTimerColor()
    {
        timerText.color = defaultColor;
        timerText.fontSize = defaultFontSize;
    }
}


