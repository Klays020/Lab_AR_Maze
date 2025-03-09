using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private GameObject endGameObject;

    [Header("������ � ����")]
    public int coinsCollected;          // ��������� ������
    public int totalScore;              // ����� ����, ������������ �� �������
    public int pointsPerCoin = 20;      // ���� �� ������
    public int pointsPerSecond = 1;     // ���� �� ���������� ����� (�� ������ �������)

    [Header("�������")]
    public float countdownTime;
    public float startCountdownTime;
    public float levelTime;
    private bool levelActive = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (levelActive)
        {
            levelTime += Time.deltaTime;
            //Debug.Log("levelTime " + levelTime);

            countdownTime -= Time.deltaTime;
            //Debug.Log("countdownTime " + countdownTime);

            if (countdownTime <= 0)
            {
                countdownTime = 0;
                EndLevel(false);
            }
        }
        else
        {
            //Debug.Log("levelActive " + levelActive);
        }
    }

    public void StartLevel()
    {
        Debug.Log("StartLevel() ");
        ResetLevelStats();
        levelActive = true;
    }

    // ����� ���������� ������
    public void ResetLevelStats()
    {
        coinsCollected = 0;
        totalScore = 0;
        levelTime = 0;

        // ������������� �������� ������ � ����������� �� ���������
        switch (GameSettings.selectedDifficulty)
        {
            case Difficulty.Easy:
                countdownTime = 30f;
                break;
            case Difficulty.Medium:
                countdownTime = 50f;
                break;
            case Difficulty.Hard:
                countdownTime = 90f;
                break;
            default:
                countdownTime = 120f;
                break;
        }

        startCountdownTime = countdownTime;
    }

    public void AddCoin()
    {
        coinsCollected++;
        Debug.Log("������ �������! �����: " + coinsCollected);
    }

    public void EndLevel(bool success)
    {
        levelActive = false;
        if (success)
        {
            // ������ ������ �����:
            // ����� ���� = (���������� ����� * pointsPerSecond) + (��������� ������ * pointsPerCoin)
            int remainingTimeScore = Mathf.RoundToInt(countdownTime * pointsPerSecond);
            totalScore = remainingTimeScore + (coinsCollected * pointsPerCoin);
            Debug.Log("������� �������! ����� �����������: " + levelTime + " ���, " +
                      "����� �������: " + coinsCollected + ", ����: " + totalScore);
            SaveResults(levelTime, totalScore);

            UITimer timerUI = FindObjectOfType<UITimer>();
            if (timerUI != null)
            {
                timerUI.ResetTimerColor();
            }
        }
        else
        {
            Debug.Log("����� �����. ������� ��������.");
        }

        if (endGameObject != null)
        {
            endGameObject.SetActive(true);
            FinalOutcomeSelector selector = endGameObject.GetComponent<FinalOutcomeSelector>();

            if (selector != null)
            {
                // isVictory = true ���� ����� �������, ����� false
                selector.ActivateOutcome(success);
            }
            else
            {
                Debug.LogWarning("FinalOutcomeSelector �� ������ �� ENDGameObject.");
            }
        }
        else
        {
            Debug.LogWarning("ENDGameObject �� ������ � �����.");
        }

    }

    // ��������� ���������� � PlayerPrefs ��� ������ ��������� ��������
    public void SaveResults(float time, int score)
    {
        // ���������� �����, ��������� �� ���������
        string diff = GameSettings.selectedDifficulty.ToString(); // ��������, "Easy"
        string lastTimeKey = "LastTime_" + diff;
        string lastScoreKey = "LastScore_" + diff;
        string bestTimeKey = "BestTime_" + diff;
        string bestScoreKey = "BestScore_" + diff;

        PlayerPrefs.SetFloat(lastTimeKey, time);
        PlayerPrefs.SetInt(lastScoreKey, score);

        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);
        int bestScore = PlayerPrefs.GetInt(bestScoreKey, 0);

        if (score > bestScore)
        {
            PlayerPrefs.SetInt(bestScoreKey, score);
            PlayerPrefs.SetFloat(bestTimeKey, time);
        }
        else if (score == bestScore && time < bestTime)
        {
            PlayerPrefs.SetFloat(bestTimeKey, time);
        }

        PlayerPrefs.Save();
        Debug.Log($"��������� ���������� ��� {diff}: LastTime = {PlayerPrefs.GetFloat(lastTimeKey)}, " +
                  $"LastScore = {PlayerPrefs.GetInt(lastScoreKey)}, BestTime = {PlayerPrefs.GetFloat(bestTimeKey)}, " +
                  $"BestScore = {PlayerPrefs.GetInt(bestScoreKey)}");
    }

    // ������ ��� ������������ ������������ (����� ���������� �� ��������� UI)
    public void TeleportBallToFinish(GameObject ball)
    {
        FinishTileController finishTile = FindObjectOfType<FinishTileController>();
        if (finishTile != null)
        {
            finishTile.TriggerWin();
            Debug.Log("������� ������� ������.");
            EndLevel(true);
        }
        else
        {
            Debug.LogWarning("�������� ������ �� �������.");
        }
    }



    public void TeleportBallToCoin(GameObject ball)
    {
        GameObject coin = GameObject.FindWithTag("Coin");
        if (coin != null && ball != null)
        {
            Debug.Log("�������� �������: " + coin.name);
            Instance.AddCoin();

            // ������� ������� �� �����
            Destroy(coin);
            Debug.Log("������� ������� ����� �������. ����� �����: " + Instance.coinsCollected);
        }
        else
        {
            Debug.LogWarning("������� �� ������� ��� ������� �������� ������ ����.");
        }
    }


    // ��� �������� ���������� ���������� ����� ������� ������ ������
    public void PrintSavedResults()
    {
        string diff = GameSettings.selectedDifficulty.ToString();
        Debug.Log("Saved Results for " + diff + ":");
        Debug.Log("LastTime: " + PlayerPrefs.GetFloat("LastTime_" + diff));
        Debug.Log("LastScore: " + PlayerPrefs.GetInt("LastScore_" + diff));
        Debug.Log("BestTime: " + PlayerPrefs.GetFloat("BestTime_" + diff));
        Debug.Log("BestScore: " + PlayerPrefs.GetInt("BestScore_" + diff));
    }
}
