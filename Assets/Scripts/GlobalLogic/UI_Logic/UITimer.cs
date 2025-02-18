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

    private float maxTime; // ������������ ����� ��� ������� ���������
    private float defaultFontSize;

    private void Start()
    {
        defaultFontSize = timerText.fontSize;

        // �������� ����� �������� �������� �� ScoreManager
        if (ScoreManager.Instance != null && ScoreManager.Instance.startCountdownTime > 0)
        {
            maxTime = ScoreManager.Instance.startCountdownTime;
        }
        else
        {
            maxTime = 0;
            Debug.Log("maxTime = 0, ������� ������������� ScoreManager.");
            timerText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // ���� maxTime ��� �� ����������, ������� �������� ��� �� ScoreManager
        if (maxTime <= 0 && ScoreManager.Instance != null && ScoreManager.Instance.startCountdownTime > 0)
        {
            maxTime = ScoreManager.Instance.startCountdownTime;
            Debug.Log("maxTime ��������: " + maxTime);
            timerText.gameObject.SetActive(true);
        }

        if (ScoreManager.Instance != null)
        {
            float time = ScoreManager.Instance.countdownTime;
            timerText.text = Mathf.CeilToInt(time).ToString();

            // ���������� maxTime ��� ������� ��������� ����������� �������
            float timePercentage = (maxTime > 0) ? (time / maxTime) : 1f; // ���� maxTime ��� 0, �������, ��� 100% �������

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


