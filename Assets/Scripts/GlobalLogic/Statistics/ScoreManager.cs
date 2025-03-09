using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private GameObject endGameObject;

    [Header("Монеты и очки")]
    public int coinsCollected;          // Собранные монеты
    public int totalScore;              // Общий счёт, рассчитанный по формуле
    public int pointsPerCoin = 20;      // Очки за монету
    public int pointsPerSecond = 1;     // Очки за оставшееся время (за каждую секунду)

    [Header("Таймеры")]
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

    // Сброс статистики уровня
    public void ResetLevelStats()
    {
        coinsCollected = 0;
        totalScore = 0;
        levelTime = 0;

        // Устанавливаем обратный отсчёт в зависимости от сложности
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
        Debug.Log("Монета собрана! Всего: " + coinsCollected);
    }

    public void EndLevel(bool success)
    {
        levelActive = false;
        if (success)
        {
            // Расчет общего счета:
            // Общий счет = (оставшееся время * pointsPerSecond) + (собранные монеты * pointsPerCoin)
            int remainingTimeScore = Mathf.RoundToInt(countdownTime * pointsPerSecond);
            totalScore = remainingTimeScore + (coinsCollected * pointsPerCoin);
            Debug.Log("Уровень пройден! Время прохождения: " + levelTime + " сек, " +
                      "Монет собрано: " + coinsCollected + ", Счет: " + totalScore);
            SaveResults(levelTime, totalScore);

            UITimer timerUI = FindObjectOfType<UITimer>();
            if (timerUI != null)
            {
                timerUI.ResetTimerColor();
            }
        }
        else
        {
            Debug.Log("Время вышло. Уровень проигран.");
        }

        if (endGameObject != null)
        {
            endGameObject.SetActive(true);
            FinalOutcomeSelector selector = endGameObject.GetComponent<FinalOutcomeSelector>();

            if (selector != null)
            {
                // isVictory = true если игрок победил, иначе false
                selector.ActivateOutcome(success);
            }
            else
            {
                Debug.LogWarning("FinalOutcomeSelector не найден на ENDGameObject.");
            }
        }
        else
        {
            Debug.LogWarning("ENDGameObject не найден в сцене.");
        }

    }

    // Сохраняем статистику в PlayerPrefs для каждой сложности отдельно
    public void SaveResults(float time, int score)
    {
        // Используем ключи, зависящие от сложности
        string diff = GameSettings.selectedDifficulty.ToString(); // Например, "Easy"
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
        Debug.Log($"Сохранены результаты для {diff}: LastTime = {PlayerPrefs.GetFloat(lastTimeKey)}, " +
                  $"LastScore = {PlayerPrefs.GetInt(lastScoreKey)}, BestTime = {PlayerPrefs.GetFloat(bestTimeKey)}, " +
                  $"BestScore = {PlayerPrefs.GetInt(bestScoreKey)}");
    }

    // Методы для тестирования телепортации (будут вызываться из тестового UI)
    public void TeleportBallToFinish(GameObject ball)
    {
        FinishTileController finishTile = FindObjectOfType<FinishTileController>();
        if (finishTile != null)
        {
            finishTile.TriggerWin();
            Debug.Log("Вызвано событие победы.");
            EndLevel(true);
        }
        else
        {
            Debug.LogWarning("Финишная плитка не найдена.");
        }
    }



    public void TeleportBallToCoin(GameObject ball)
    {
        GameObject coin = GameObject.FindWithTag("Coin");
        if (coin != null && ball != null)
        {
            Debug.Log("Собираем монетку: " + coin.name);
            Instance.AddCoin();

            // Удаляем монетку из сцены
            Destroy(coin);
            Debug.Log("Монетка удалена после подбора. Всего монет: " + Instance.coinsCollected);
        }
        else
        {
            Debug.LogWarning("Монетка не найдена или передан неверный объект шара.");
        }
    }


    // Для проверки сохранения статистики можно создать методы чтения
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
